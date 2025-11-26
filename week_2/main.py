# Part 1
total = 0
with open("input.txt") as f:
    for line in f:
        line = line.strip()
        for c in line:
            if c.isdigit():
                first = int(c)
                break
        for c in reversed(line):
            if c.isdigit():
                last = int(c)
                break
        total += first * 10 + last
print("Part 1:", total)

# Part 2
digit_words = {
    "one": 1,
    "two": 2,
    "three": 3,
    "four": 4,
    "five": 5,
    "six": 6,
    "seven": 7,
    "eight": 8,
    "nine": 9,
}
total = 0
with open("input.txt") as f:
    for line in f:
        line = line.strip()
        if not line:
            continue
        first = None
        last = None
        for i in range(len(line)):
            d = None
            if line[i].isdigit():
                d = int(line[i])
            else:
                for word, val in digit_words.items():
                    if line.startswith(word, i):
                        d = val
                        break
            if d is not None:
                if first is None:
                    first = d
                last = d
        total += first * 10 + last
print("Part 2:", total)