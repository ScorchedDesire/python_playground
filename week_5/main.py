# Part 1
score_shape = {"X":1,"Y":2,"Z":3}
to_num = {"A":0,"B":1,"C":2,"X":0,"Y":1,"Z":2}
total = 0
with open("input.txt") as f:
    for line in f:
        a,b = line.split()
        op = to_num[a]
        me = to_num[b]
        total += score_shape[b]
        if op == me: total += 3
        elif (me - op) % 3 == 1: total += 6
print("Part 1:", total)

# Part 2
to_num={"A":0,"B":1,"C":2}
outcome={"X":0,"Y":3,"Z":6}
total=0
with open("input.txt") as f:
    for line in f:
        a,b=line.split()
        op=to_num[a]
        if b=="Y": me=op
        elif b=="Z": me=(op+1)%3
        else: me=(op+2)%3
        total+=me+1+outcome[b]
print("Part 2:", total)