# Disk_use.c

Disk_use.c will take a mountpoint on the filesystem and a given threshold for disk usage. It will then calculate the disk usage of that mount point and if greater than or equal to the threshold write (or append) an error message to a log file on the system.

## Program/Code Walkthrough
The following snippet ensures that when the command is run only a mountpoint and threshold value are given
```c
	if(argc != 3)
	{
		printf("Usage command [Mount Pont] [Threshold]\n");
		return -1;
	}
```
This section uses the `statvfs()` call to check if the given filepath/mount point exists/extracts the information into the `statvfs struct` named `buffer`.

From here the percentage of disk usage is calculated by determining the available blocks divided by the total number of blocks converted to a percent.

If `used_percent` is greater than or equal to `threshold`, the current time is obtained from the `time_t` type in the `time.h` library and hen converted to a string.

A file is then opened (if doesn't exist created) named `disk_usage_alert.log` in the `/var/log/` directory and an error message is printed that includes the % Usage, the mountpoint, and the timestamp the message was created.

```c
	if(statvfs(argv[1], &buffer) == 0)
	{
		double used_percent = 100.0 * (1.0 - ((double)buffer.f_bavail / buffer.f_blocks));

		if(used_percent >= threshold)
		{
			time_t current_time = time(NULL);
			char* date_time_string = ctime(&current_time);

			FILE *log = fopen("/var/log/disk_usage_alert.log", "a");

			fprintf(log, "ALERT: Disk usage at %.2f%% on %s at %s\n", used_percent, argv[1], date_time_string);

			fclose(log);
		}
	}
```

## Usage
The disk_usage binary can be used by providing a mountpoint/filepath and a threshold value.

Ex: User wants to check /System/Volumes/Data for usage greater than 45.5 % they would run: `disk_use /System/Volumes/Data 45.5`

Should the usage be greater than that threshold then there will be a `/var/log/disk_usage_alert.log` file that will be created or have the message `"ALERT: Disk usage at 47.5% on /System/Volume/Data at Thu Oct 24 16:48:46 2024"`
