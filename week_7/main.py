# Part 1
h = 0
d = 0
with open("input.txt") as f:
    for line in f:
        cmd, x = line.split()
        x = int(x)
        if cmd == "forward": h += x
        elif cmd == "down": d += x
        else: d -= x
print("Part 1:", h * d)

# Part 2
h = 0
d = 0
a = 0
with open("input.txt") as f:
    for line in f:
        cmd, x = line.split()
        x = int(x)
        if cmd == "forward":
            h += x
            d += a * x
        elif cmd == "down":
            a += x
        else:
            a -= x
print("Part 2:", h * d)