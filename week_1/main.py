# Part 1
left = []
right = []
with open("input.txt") as f:
    for line in f:
        if line.strip():
            a, b = map(int, line.split())
            left.append(a)
            right.append(b)
left.sort()
right.sort()
total_distance = sum(abs(a - b) for a, b in zip(left, right))
print("Part 1:", total_distance)

# Part 2
left = []
right = []
with open("input.txt") as f:
    for line in f:
        if line.strip():
            a, b = map(int, line.split())
            left.append(a)
            right.append(b)
similarity = 0
for x in left:
    count = 0
    for r in right:
        if r == x:
            count += 1
    similarity += x * count
print("Part 2:", similarity)