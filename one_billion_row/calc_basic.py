import time

start_time = time.time()
stats = {}

file = open("measurements.txt", "r")

# Split each line into parts and perform the calculations
for line in file:
    city, temp = line.strip().split(";")
    temperature = float(temp)

    if city not in stats:
        # Initializing stats for the city
        stats[city] = [1, temperature, temperature, temperature] # count, total, min, max
    else:
        record = stats[city]
        record[0] += 1 # increment count
        record[1] += temperature # add to total
        record[2] = min(record[2], temperature) # min temperature
        record[3] = max(record[3], temperature) # max temperature

end_time = time.time()
file.close()

for city in sorted(stats):
    count, total, minimum, maximum = stats[city]
    average = total / count
    print(f"{city} = {minimum} | {average} | {maximum}")

print(f"\nTime taken: {end_time - start_time} seconds")

# Time taken:
# 10 Mil: 7.26s
# 1 Billion: 664s (11m04s)
# (Ryzen 5 7640HS, laptop)
