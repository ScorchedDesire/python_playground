# Part 1
import re
monkeys = []
with open("input.txt") as f:
    blocks = f.read().split("\n\n")
for block in blocks:
    lines = block.strip().splitlines()
    items = list(map(int, re.findall(r"\d+", lines[1])))
    op = lines[2].split("= ")[1]
    div = int(re.findall(r"\d+", lines[3])[0])
    t = int(re.findall(r"\d+", lines[4])[0])
    f = int(re.findall(r"\d+", lines[5])[0])
    monkeys.append({"items": items[:], "op": op, "div": div, "t": t, "f": f, "count": 0})
for _ in range(20):
    for m in monkeys:
        for item in m["items"]:
            m["count"] += 1
            old = item
            new = eval(m["op"])
            new //= 3
            if new % m["div"] == 0:
                monkeys[m["t"]]["items"].append(new)
            else:
                monkeys[m["f"]]["items"].append(new)
        m["items"] = []
counts = sorted([m["count"] for m in monkeys], reverse=True)
print("Part 1:", counts[0] * counts[1])

# Part 2
monkeys = []
with open("input.txt") as f:
    blocks = f.read().split("\n\n")
for block in blocks:
    lines = block.strip().splitlines()
    items = list(map(int, re.findall(r"\d+", lines[1])))
    op = lines[2].split("= ")[1]
    div = int(re.findall(r"\d+", lines[3])[0])
    t = int(re.findall(r"\d+", lines[4])[0])
    f = int(re.findall(r"\d+", lines[5])[0])
    monkeys.append({"items": items[:], "op": op, "div": div, "t": t, "f": f, "count": 0})
mod = 1
for m in monkeys:
    mod *= m["div"]
for _ in range(10000):
    for m in monkeys:
        for item in m["items"]:
            m["count"] += 1
            old = item
            new = eval(m["op"])
            new %= mod
            if new % m["div"] == 0:
                monkeys[m["t"]]["items"].append(new)
            else:
                monkeys[m["f"]]["items"].append(new)
        m["items"] = []
counts = sorted([m["count"] for m in monkeys], reverse=True)
print("Part 2:", counts[0] * counts[1])