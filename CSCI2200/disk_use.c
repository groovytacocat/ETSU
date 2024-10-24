#include<stdio.h>
#include<stdint.h>
#include<string.h>
#include<stdlib.h>
#include<sys/statvfs.h>
#include<time.h>

int main(int argc, char* argv[])
{
	struct statvfs buffer;

	if(argc != 3)
	{
		printf("Usage command [Mount Pont] [Threshold]\n");
		return -1;
	}

	printf("Chosen Dir:\n%s\nThreshold: %.2f\n", argv[1], atof(argv[2]));

	double threshold = atof(argv[2]);

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

	return 0;
}
