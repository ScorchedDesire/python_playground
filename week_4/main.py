# Part 1
import hashlib
salt = "jlmsuwbz"
def md5(s):
    return hashlib.md5(s.encode()).hexdigest()
def first_triplet(h):
    for i in range(len(h) - 2):
        if h[i] == h[i+1] == h[i+2]:
            return h[i]
    return None
def has_five(h, c):
    return c * 5 in h
keys = []
i = 0
hash_cache = {}
while len(keys) < 64:
    if i not in hash_cache:
        hash_cache[i] = md5(salt + str(i))
    h = hash_cache[i]
    c = first_triplet(h)
    if c:
        for j in range(i+1, i+1001):
            if j not in hash_cache:
                hash_cache[j] = md5(salt + str(j))
            if has_five(hash_cache[j], c):
                keys.append(i)
                break
    i += 1
print("Part 1:", keys[63])

# Part 2
import hashlib
salt = "jlmsuwbz"
hash_cache = {}
def stretched_md5(s: str) -> str:
    h = hashlib.md5(s.encode()).hexdigest()
    for _ in range(2016):
        h = hashlib.md5(h.encode()).hexdigest()
    return h
def first_triplet(h: str):
    for i in range(len(h) - 2):
        if h[i] == h[i+1] == h[i+2]:
            return h[i]
    return None
def has_five(h: str, c: str) -> bool:
    return c * 5 in h
def get_hash(i: int) -> str:
    if i not in hash_cache:
        hash_cache[i] = stretched_md5(salt + str(i))
    return hash_cache[i]
keys = []
i = 0
while len(keys) < 64:
    h = get_hash(i)
    c = first_triplet(h)
    if c:
        for j in range(i + 1, i + 1001):
            if has_five(get_hash(j), c):
                keys.append(i)
                break
    i += 1
print("Part 2:", keys[63])