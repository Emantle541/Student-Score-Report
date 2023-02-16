using System;
using System.IO;

namespace StudentScoreReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName;
            string file;
            userName = getName();
            Console.WriteLine($"Hello {userName}!");
            file = GetFileName("Enter the name of the file");
            //getfileinfo(file);
            StreamReader fileReader = new StreamReader(file);
            int students = 0;
            double min = 100;
            double max = 0;
            double fileaverage = 0;
            while(!fileReader.EndOfStream)
            {
                string lineOfData = fileReader.ReadLine();
                string[] data = lineOfData.Split(",");
                string firstname = data[0];
                string lastname = data[1];
                int creditHours = int.Parse(data[2]);
                string major = data[3];
                double exam1_score = double.Parse(data[4]);
                double exam2_score = double.Parse(data[5]);
                double exam3_score = double.Parse(data[6]);
                string year = getyear(creditHours);
                double examaverage = getaverageGrade(exam1_score,exam2_score,exam3_score);
                double oldmin = findmin(exam1_score, exam2_score, exam3_score);
                double oldmax = findmax(exam1_score,exam2_score,exam3_score);
                if(min > oldmin)
                {
                    min = oldmin;
                }
                if(max < oldmax)
                {
                    max = oldmax;
                }

                Console.WriteLine($"Name: {firstname} {lastname}: {year}({major})");
                Console.WriteLine($"Average Score: {examaverage}%\n");
                students++;
                fileaverage += examaverage;
            }
            double average = fileaverage/students;
            double finalaverage =Math.Round(average,2);
            using(StreamWriter fileWriter = File.CreateText("scoresLog.txt"))
          {
            try
            {
                fileWriter.WriteLine($"Number of Students: {students}");
                fileWriter.WriteLine($"Average Grade: %{finalaverage}");
                fileWriter.WriteLine($"Average Highest Grade: %{max}");
                fileWriter.WriteLine($"Average Lowest Grade: %{min}");
                Console.WriteLine("The scoresLog.txt file was successfully written.");
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                fileWriter.Close();
            }
        
          }
            Console.ReadLine();
        }
        
        

        static double findmin(double exam1, double exam2, double exam3)
        {
            double total = exam1+exam2+exam3;
            double minimum = total/3;
            double newminimum = Math.Round(minimum,2);
            
            return(newminimum);
        }

        static double findmax(double exam1, double exam2, double exam3)
        {
            double total = exam1+exam2+exam3;
            double maximum = total/3;
            double newmaximum = Math.Round(maximum,2);
            
            return(newmaximum);
        }

        static double getaverageGrade(double exam1, double exam2, double exam3)
        {
            double totalgrade = exam1 += exam2 += exam3;
            double average=totalgrade/3;
            double newaverage=Math.Round(average,2);
            return(newaverage);
        }

        static string getyear(float credits){
            //(A = 90-100, B = 80-89, C = 70-79, D = 60 - 69, F lower than 60).
            if (credits >= 90){
                return "Senior";
            }else if(credits >= 60){
                return "Junior";
            }else if (credits >= 30){
                return "Sophomore";
            }else{
                return "Freshman";
            }
        }

        static string getName()
        //Function to get the person's names!
        {
            Console.WriteLine("Please enter your first name and last name");
            string userName = Console.ReadLine();

            return userName;
        }

        static string GetFileName(string userPrompt){
            string userFileName;
            while (true){
                Console.WriteLine(userPrompt);
                userFileName = Console.ReadLine();

                if(userFileName == ""){
                    Console.WriteLine("ERROR: Must enter a file name");
                    continue;
                }else if (!userFileName.EndsWith(".csv")){
                    userFileName += ".csv";
                }
                
                if(!File.Exists(userFileName)){
                    Console.WriteLine($"ERROR: {userFileName} does not exist! ReEnter file name"); 
                    continue;
                }else{
                    return userFileName;
                }
            }       
        }
    }
}
