#include <fstream>
#include <NeuralNetwork.h>
#include <conio.h>

using namespace std;

double* Parse(double Idle[], int ind)
{
	ifstream  file;
	file.open("ProcessedDumpCollections.json");
	double* input = new double[4];
	int* KBK = new int[20];
	char Buf[30] = { '\0' };
	int iter = 0;
	if (file.is_open())
	{
		file >> Buf;
		while (!file.eof()&&iter!=ind)
		{
			for (int i = 0; i < 75; i++)Idle[i] = 0.2;
			file >> Buf;
			file >> Buf;
			file >> Buf;
			int i = 0;
			while (true)
			{
				file >> Buf;
				if (Buf[0] == ']' || file.eof())
					break;
				KBK[i++] = atof(Buf);
			}
			if (file.eof())
				break;
			file >> Buf;
			file >> Buf;
			int j = 0;
			for (; Buf[j + 1] != '\0'; j++)
				Buf[j] = Buf[j + 1];
			if (j >= 2)
				Buf[j - 2] = '\0';
			input[0] = atof(Buf);
			input[0] = (int)input[0] % 10000;
			input[0] /= 1000;
			file >> Buf;
			file >> Buf;
			j = 0;
			while (Buf[j] != '\0')j++;
			Buf[j - 1] = '\0';
			input[1] = atof(Buf);
			file >> Buf;
			file >> Buf;
			while (Buf[j] != '\0')j++;
			Buf[j - 1] = '\0';
			input[2] = atof(Buf);
			file >> Buf;
			file >> Buf;
			j = 0;
			for (; Buf[j + 1] != '\0'; j++)
				Buf[j] = Buf[j + 1];
			Buf[j - 1] = '\0';
			input[3] = atof(Buf);
			file >> Buf;
			for (j = 0; j < i; j++)
				Idle[KBK[j]] = 0.8;
			double max = 0;
			int k = 0;
			iter++;
		}
		file.close();
	}
	return input;
}

int main()
{
	srand((int)time(0));
	char Text[] = { "Net.txt" };
	NeuralNetwork* Net = new NeuralNetwork(Text);
	bool cont = true;
	int Lays[4] = { 128,128,120,75 };
	//Net = new NeuralNetwork(4, Lays, 0.7);
	double Idle[75] = { 0 };
	int iter = 0;
	double epsilon = 0.1;
	while (true)
	{
		cont = false;
		Net->Study(Parse(Idle, rand() % 1700 + 1), Idle, 4);
		iter++;
		for(int count=0; count<100; count++)
		{ 
			if (abs(Net->MaxError()) > epsilon)
			{
				cont = true;
				break;
			}
			iter++;
			if (iter % 1000==0)
			{
				epsilon += 0.01;
				printf("%d", iter);
			}
			Net->Study(Parse(Idle, rand() % 1700 + 1), Idle, 4);
		}
		if (iter % 1000==0)
		{
			epsilon += 0.01;
		}
		printf("%d\n", iter);
		if (cont)
			continue;
		else
			break;
		_getch();
	}
	printf("MAX ERROR: %f EPSILON: %f\n", Net->MaxError(), epsilon);
	Net->Save(Text);
}