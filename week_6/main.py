# Part 1
wires = {}
with open("input.txt") as f:
    for line in f:
        expr, out = map(str.strip, line.split("->"))
        wires[out] = expr.strip()
memo = {}
def get(x):
    if x.isdigit():
        return int(x)
    if x in memo:
        return memo[x]
    expr = wires[x].split()
    if len(expr) == 1:
        val = get(expr[0])
    elif len(expr) == 2:
        val = ~get(expr[1]) & 0xFFFF
    else:
        a, op, b = expr
        if op == "AND":
            val = get(a) & get(b)
        elif op == "OR":
            val = get(a) | get(b)
        elif op == "LSHIFT":
            val = (get(a) << int(b)) & 0xFFFF
        else:
            val = get(a) >> int(b)
    memo[x] = val
    return val
a_val = get("a")
print("Part 1:", a_val)

# Part 2
wires = {}
with open("input.txt") as f:
    for line in f:
        expr, out = map(str.strip, line.split("->"))
        wires[out] = expr.strip()
wires["b"] = str(a_val)
memo = {}
def get(x):
    if x.isdigit():
        return int(x)
    if x in memo:
        return memo[x]
    expr = wires[x].split()
    if len(expr) == 1:
        val = get(expr[0])
    elif len(expr) == 2:
        val = ~get(expr[1]) & 0xFFFF
    else:
        a, op, b = expr
        if op == "AND":
            val = get(a) & get(b)
        elif op == "OR":
            val = get(a) | get(b)
        elif op == "LSHIFT":
            val = (get(a) << int(b)) & 0xFFFF
        else:
            val = get(a) >> int(b)
    memo[x] = val
    return val
print("Part 2:", get("a"))