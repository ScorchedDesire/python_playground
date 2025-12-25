import time, os
from concurrent.futures import ProcessPoolExecutor, as_completed

# Number of lines to process per batch sent to a worker
BATCH_SIZE = 500000

# Process a batch of lines and return stats per city
def process_batch(lines):
    stats = {}
    for line in lines:
        city, temp = line.strip().split(";")
        temperature = float(temp)
        if city not in stats:
            # Initializing stats for the city
            stats[city] = [1, temperature, temperature, temperature]  # count, total, min, max
        else:
            record = stats[city]
            record[0] += 1 # increment count
            record[1] += temperature # add to total
            record[2] = min(record[2], temperature) # update min
            record[3] = max(record[3], temperature) # update max
    return stats

# Merge stats from a worker batch into the global stats dictionary
def merge_stats(global_stats, local_stats):
    for city, (count, total, minimum, maximum) in local_stats.items():
        if city not in global_stats:
            global_stats[city] = [count, total, minimum, maximum]
        else:
            record = global_stats[city]
            record[0] += count
            record[1] += total
            record[2] = min(record[2], minimum)
            record[3] = max(record[3], maximum)

if __name__ == "__main__":
    filename = "measurements.txt"
    workers = os.cpu_count()  # number of CPU cores

    start_time = time.perf_counter()  # start timer
    final_stats = {}

    # Open the file with 1MB buffer and create a pool of workers
    with open(filename, "r", buffering=1024*1024) as f, ProcessPoolExecutor(max_workers=workers) as executor:
        futures = []
        batch = []

        # Read file line by line and collect batches
        for idx, line in enumerate(f, 1):
            batch.append(line)
            if idx % BATCH_SIZE == 0:
                # Submit batch to a worker
                futures.append(executor.submit(process_batch, batch))
                batch = []

        # Submit remaining lines
        if batch:
            futures.append(executor.submit(process_batch, batch))

        # Merge results as workers finish
        for future in as_completed(futures):
            merge_stats(final_stats, future.result())

    # Display final results
    for city in sorted(final_stats):
        count, total, minimum, maximum = final_stats[city]
        if count > 0:
            average = total / count  
        else: 
            average = 0.0
        print(f"{city} = {minimum} | {average} | {maximum}")

    end_time = time.perf_counter()  # End timer
    print(f"\nTime taken: {end_time - start_time} seconds")
    
# Time taken: 
# 10 Mil: 3.3s
# 1 Billion: 319.48s (5m19s)
# (Ryzen 5 7640HS, laptop)