using NUnit.Framework.Legacy;
using Excel.Core.Abstraction;
using Excel.Core.Implementation.Entities;
using Excel.Core.Implementation.Classes;

namespace Excel.Tests.Tests
{
    [TestFixture]
    public class TableTests
    {
        private Table _table;

        [SetUp]
        public void Setup()
        {
            _table = new Table(5, 5);
        }

        [Test]
        public void Table_SetAndGetRawValue()
        {
            _table.SetRawValue(0, 0, "123");
            _table.SetRawValue(1, 1, 42.0);

            ClassicAssert.AreEqual("123", _table.GetRawValue(0, 0));
            ClassicAssert.AreEqual(42.0, _table.GetRawValue(1, 1));
        }

        [Test]
        public void Table_SetAndGetDisplayValue()
        {
            _table.SetDisplayValue(0, 0, "abc");
            _table.SetDisplayValue(1, 1, 3.14);

            ClassicAssert.AreEqual("abc", _table.GetDisplayValue(0, 0)?.ToString());
            ClassicAssert.AreEqual(3.14, _table.GetDisplayValue(1, 1));
        }

        private class TestObserver : ICellObserver
        {
            public int LastRow { get; private set; } = -1;
            public int LastCol { get; private set; } = -1;

            public void OnCellChanged(int row, int col)
            {
                LastRow = row;
                LastCol = col;
            }
        }

        [Test]
        public void Table_NotifiesObservers_OnSetRawValue()
        {
            var obs = new TestObserver();
            _table.AddObserver(obs);

            _table.SetRawValue(2, 3, "X");

            ClassicAssert.AreEqual(2, obs.LastRow);
            ClassicAssert.AreEqual(3, obs.LastCol);
        }
    }

    /// Tests for formula evaluation, including dependency propagation.
    [TestFixture]
    public class FormulaManagerTests
    {
        private Table _table;
        private FunctionManager _functions;
        private FormulaManager _fm;

        [SetUp]
        public void Setup()
        {
            _table = new Table(10, 10);
            _functions = new FunctionManager();
            _functions.AutoRegisterAll(); // SUM, COUNT, AVG, etc.
            _fm = new FormulaManager(_table, _functions);
        }

        [Test]
        public void EvaluateCell_PlainNumber_StoresNumber()
        {
            _table.SetRawValue(0, 0, "123");
            var result = _fm.EvaluateCell(0, 0);

            ClassicAssert.AreEqual(123.0, result);
            ClassicAssert.AreEqual(123.0, _table.GetDisplayValue(0, 0));
        }

        [Test]
        public void EvaluateCell_Text_NoFormula_StoresText()
        {
            _table.SetRawValue(0, 0, "hello");
            var result = _fm.EvaluateCell(0, 0);

            ClassicAssert.AreEqual("hello", result);
            ClassicAssert.AreEqual("hello", _table.GetDisplayValue(0, 0));
        }

        [Test]
        public void EvaluateCell_SimpleAddition_Recalculates()
        {
            // A1 = 2, B1 = 3, C1 = =A1+B1
            _table.SetRawValue(0, 0, "2");     // A1
            _table.SetRawValue(0, 1, "3");     // B1
            _table.SetRawValue(0, 2, "=A1+B1");// C1

            var c1 = _fm.EvaluateCell(0, 2);
            ClassicAssert.AreEqual(5.0, c1);
            ClassicAssert.AreEqual(5.0, _table.GetDisplayValue(0, 2));

            // Now change A1 to 10 — FormulaManager is subscribed as observer
            _table.SetRawValue(0, 0, "10");

            // C1 should be recalculated automatically
            ClassicAssert.AreEqual(13.0, _table.GetDisplayValue(0, 2));
        }

        [Test]
        public void EvaluateCell_Function_SUM()
        {
            // A1=1, A2=2, A3=3, B1=4  => SUM(A1:A3) + B1 = 1+2+3+4 = 10
            _table.SetRawValue(0, 0, "1");   // A1
            _table.SetRawValue(1, 0, "2");   // A2
            _table.SetRawValue(2, 0, "3");   // A3
            _table.SetRawValue(0, 1, "4");   // B1
            _table.SetRawValue(0, 2, "=SUM(A1:A3)+B1"); // C1

            var c1 = _fm.EvaluateCell(0, 2);

            ClassicAssert.AreEqual(10.0, c1);
            ClassicAssert.AreEqual(10.0, _table.GetDisplayValue(0, 2));
        }

        [Test]
        public void EvaluateCell_Functions_COUNT_AVG()
        {
            // A1=1, A2=2, A3="x"  => COUNT(A1:A3) = 2, AVG(A1:A2) = 1.5
            _table.SetRawValue(0, 0, "1");
            _table.SetRawValue(1, 0, "2");
            _table.SetRawValue(2, 0, "x");

            _table.SetRawValue(0, 1, "=COUNT(A1:A3)"); // B1
            _table.SetRawValue(1, 1, "=AVG(A1:A2)");   // B2

            var b1 = _fm.EvaluateCell(0, 1);
            var b2 = _fm.EvaluateCell(1, 1);

            ClassicAssert.AreEqual(2, b1);
            ClassicAssert.AreEqual(1.5, b2);
        }

        [Test]
        public void ChangingSourceCell_RecalculatesDependentRangeFormula()
        {
            // A1=1, A2=1, A3=1, B1=SUM(A1:A3)
            _table.SetRawValue(0, 0, "1");
            _table.SetRawValue(1, 0, "1");
            _table.SetRawValue(2, 0, "1");
            _table.SetRawValue(0, 1, "=SUM(A1:A3)");

            var initial = _fm.EvaluateCell(0, 1);
            ClassicAssert.AreEqual(3.0, initial);

            // Change A2 -> 10, dependent SUM should update to 12
            _table.SetRawValue(1, 0, "10");
            _fm.EvaluateCell(0, 1);

            ClassicAssert.AreEqual(12.0, _table.GetDisplayValue(0, 1));
        }
    }

    [TestFixture]
    public class FileAndExportTests
    {
        private string _tempDir = null!;

        [SetUp]
        public void Setup()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), "ExcelTests_" + Guid.NewGuid());
            Directory.CreateDirectory(_tempDir);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_tempDir))
            {
                try { Directory.Delete(_tempDir, true); } catch { /* ignore */ }
            }
        }

        private string TempFile(string name) => Path.Combine(_tempDir, name);

        [Test]
        public void FileManager_Save_And_Load_RoundTrip()
        {
            var table = new Table(2, 2);
            table.SetRawValue(0, 0, "1");
            table.SetRawValue(0, 1, "hello");
            table.SetRawValue(1, 0, "=A1+1");
            table.SetRawValue(1, 1, null);

            var fm = new FileManager();
            var path = TempFile("test.exl");

            fm.Save(path, table);

            Assert.That(File.Exists(path), Is.True, "File should exist after Save");

            var loaded = fm.Load(path);

            ClassicAssert.AreEqual(2, loaded.RowCount);
            ClassicAssert.AreEqual(2, loaded.ColumnCount);
            ClassicAssert.AreEqual("1", loaded.GetRawValue(0, 0));
            ClassicAssert.AreEqual("hello", loaded.GetRawValue(0, 1));
            ClassicAssert.AreEqual("=A1+1", loaded.GetRawValue(1, 0));
            ClassicAssert.IsNull(loaded.GetRawValue(1, 1));
        }

        [Test]
        public void Export_Csv_Then_ImportCsv_RoundTrip()
        {
            var table = new Table(2, 2);
            table.SetDisplayValue(0, 0, "10");
            table.SetDisplayValue(0, 1, "20");
            table.SetDisplayValue(1, 0, "hello");
            table.SetDisplayValue(1, 1, "world");

            var export = new ExportManager();
            var import = new ImportManager();

            var path = TempFile("test.csv");
            export.ExportCsv(path, table);

            Assert.That(File.Exists(path), Is.True);

            var imported = import.ImportCsv(path);

            ClassicAssert.AreEqual(2, imported.RowCount);
            ClassicAssert.AreEqual(2, imported.ColumnCount);

            ClassicAssert.AreEqual("10", imported.GetRawValue(0, 0));
            ClassicAssert.AreEqual("20", imported.GetRawValue(0, 1));
            ClassicAssert.AreEqual("hello", imported.GetRawValue(1, 0));
            ClassicAssert.AreEqual("world", imported.GetRawValue(1, 1));
        }

        [Test]
        public void Export_Json_Then_ImportJson_RoundTrip()
        {
            var table = new Table(2, 2);
            table.SetDisplayValue(0, 0, "1");
            table.SetDisplayValue(0, 1, "2");
            table.SetDisplayValue(1, 0, "foo");
            table.SetDisplayValue(1, 1, "bar");

            var export = new ExportManager();
            var import = new ImportManager();

            var path = TempFile("test.json");
            export.ExportJson(path, table);

            Assert.That(File.Exists(path), Is.True);

            var imported = import.ImportJson(path);

            ClassicAssert.AreEqual("1", imported.GetRawValue(0, 0));
            ClassicAssert.AreEqual("2", imported.GetRawValue(0, 1));
            ClassicAssert.AreEqual("foo", imported.GetRawValue(1, 0));
            ClassicAssert.AreEqual("bar", imported.GetRawValue(1, 1));
        }

        [Test]
        public void Export_Xls_Then_ImportXls_RoundTrip_ValuesOnly()
        {
            var table = new Table(1, 2);
            table.SetRawValue(0, 0, "=A2+1");   
            table.SetDisplayValue(0, 0, "5");  
            table.SetRawValue(0, 1, "3");
            table.SetDisplayValue(0, 1, "3");

            var export = new ExportManager();
            var import = new ImportManager();

            var path = TempFile("test.xls");
            export.ExportXls(path, table);

            Assert.That(File.Exists(path), Is.True);

            var imported = import.ImportXls(path);

            ClassicAssert.AreEqual("5", imported.GetRawValue(0, 0));
            ClassicAssert.AreEqual("3", imported.GetRawValue(0, 1));
        }
    }

    [TestFixture]
    public class SessionManagerTests
    {
        private string _originalDir = null!;

        [SetUp]
        public void Setup()
        {
            _originalDir = Directory.GetCurrentDirectory();
            var tmp = Path.Combine(Path.GetTempPath(), "ExcelSessionTests_" + Guid.NewGuid());
            Directory.CreateDirectory(tmp);
            Directory.SetCurrentDirectory(tmp);
        }

        [TearDown]
        public void TearDown()
        {
            var current = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(_originalDir);

            try { Directory.Delete(current, true); } catch { /* ignore */ }
        }

        [Test]
        public void SessionManager_SaveLoadClear_Works()
        {
            var sm = new SessionManager();

            ClassicAssert.IsFalse(sm.HasToken);

            sm.SaveToken("abc123");
            ClassicAssert.IsTrue(sm.HasToken);
            ClassicAssert.AreEqual("abc123", sm.LoadToken());

            sm.ClearToken();
            ClassicAssert.IsFalse(sm.HasToken);
            ClassicAssert.IsNull(sm.LoadToken());
        }
    }
}