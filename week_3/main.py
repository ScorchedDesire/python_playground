# Part 1
def is_safe(report):
    increasing = all(report[i] < report[i+1] for i in range(len(report)-1))
    decreasing = all(report[i] > report[i+1] for i in range(len(report)-1))
    if not (increasing or decreasing):
        return False
    for i in range(len(report)-1):
        diff = abs(report[i] - report[i+1])
        if diff < 1 or diff > 3:
            return False
    return True

safe_count = 0
with open("input.txt") as f:
    for line in f:
        nums = list(map(int, line.split()))
        if is_safe(nums):
            safe_count += 1
print("Part 1:", safe_count)

# Part 2
def is_safe(report):
    increasing = all(report[i] < report[i+1] for i in range(len(report)-1))
    decreasing = all(report[i] > report[i+1] for i in range(len(report)-1))
    if not (increasing or decreasing):
        return False
    for i in range(len(report)-1):
        if not (1 <= abs(report[i] - report[i+1]) <= 3):
            return False
    return True

def safe_with_dampener(report):
    if is_safe(report):
        return True
    for i in range(len(report)):
        modified = report[:i] + report[i+1:]
        if is_safe(modified):
            return True
    return False

safe_count = 0
with open("input.txt") as f:
    for line in f:
        report = list(map(int, line.split()))
        if safe_with_dampener(report):
            safe_count += 1
print("Part 2:", safe_count)