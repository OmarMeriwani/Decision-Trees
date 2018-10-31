using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decision_trees
{
    class Program
    {
        public static List<Attribute> ProgramAttributes = new List<Attribute>();
        static void Main(string[] args)
        {
            AttributeClass cold = new AttributeClass("cold");
            AttributeClass warm = new AttributeClass("warm");
            List<AttributeClass> TempratureClasses = new List<AttributeClass>();
            TempratureClasses.Add(cold);
            TempratureClasses.Add(warm);
            Attribute Temprature = new Attribute();
            Temprature.AttrID = 1;
            Temprature.AttrName = "Temprature";
            Temprature.classes = TempratureClasses;
            ProgramAttributes.Add(Temprature);

            AttributeClass cloudy = new AttributeClass("cloudy");
            AttributeClass clear = new AttributeClass("clear");
            AttributeClass overcast = new AttributeClass("clear");
            List<AttributeClass> SkyClasses = new List<AttributeClass>();
            SkyClasses.Add(cloudy);
            SkyClasses.Add(clear);
            SkyClasses.Add(overcast);
            Attribute Sky = new Attribute();
            Sky.AttrID = 2;
            Sky.AttrName = "Sky";
            Sky.classes = SkyClasses;
            ProgramAttributes.Add(Sky);

            AttributeClass windy = new AttributeClass("windy");
            AttributeClass calm = new AttributeClass("calm");
            List<AttributeClass> WindClasses = new List<AttributeClass>();
            WindClasses.Add(windy);
            WindClasses.Add(calm);
            Attribute Wind = new Attribute();
            Wind.AttrID = 3;
            Wind.AttrName = "Wind";
            Wind.classes = WindClasses;
            ProgramAttributes.Add(Wind);

            AttributeClass rain = new AttributeClass("rain");
            AttributeClass dry = new AttributeClass("dry");
            List<AttributeClass> HumidClasses = new List<AttributeClass>();
            HumidClasses.Add(rain);
            HumidClasses.Add(dry);
            Attribute Humidity = new Attribute();
            Humidity.AttrID = 4;
            Humidity.AttrName = "Humidity";
            Humidity.classes = HumidClasses;
            ProgramAttributes.Add(Humidity);

            Example day1 = new Example("all", "Humidity:rain;Wind:windy;Sky:cloudy;Temprature:cold;");
            Example day2 = new Example("all", "Humidity:dry;Wind:calm;Sky:clear;Temprature:warm;");
            Example day3 = new Example("all", "Humidity:rain;Wind:windy;Sky:cloudy;Temprature:cold;");
            Example day4 = new Example("all", "Humidity:dry;Wind:windy;Sky:clear;Temprature:warm;");
            Example day5 = new Example("all", "Humidity:rain;Wind:calm;Sky:cloudy;Temprature:cold;");
            List<Example> examples = new List<Example>();
            examples.Add(day1);
            examples.Add(day2);
            examples.Add(day3);
            examples.Add(day4);
            examples.Add(day5);
            string BestAttr = CalculateInformationGain(examples, "all");

        }
        public static string CalculateInformationGain (List<Example> examples, string attributeLists)
        {
            List<Attribute> attributes = new List<Attribute>();
            if (attributeLists == "" || attributeLists == "*" || attributeLists == "all")
            {
                attributes = Program.ProgramAttributes;
            }
            //If he provided a semi-colon separated list of attributes then the program will set them
            else
            {
                try
                {
                    //Split the names of the provided list
                    string[] NeededAttributeNames = attributeLists.Split(';');
                    //Add each attribute that has a compatible name with the names provided by the user to create the example
                    foreach (string s in NeededAttributeNames)
                    {
                        attributes.Add(Program.ProgramAttributes.Where(a => a.AttrName == s).SingleOrDefault());
                    }
                }
                //In case of having an error in the names of the attributes then the program will provide the user with all attributes in the program
                catch (Exception ex)
                {
                    attributes = Program.ProgramAttributes;
                }
            }
            //
            return "";
        }
        public static List<Attribute> best_attribute = new List<Attribute>();
        static Node Create_decision_tree (List<Example> examples, List<Attribute> attributes)
        {
            //Define modal class 
            Node N = new Node();
            N.Name = @"N/A";
            //If examples are all in the same class THEN RETURN N labelled with that class
            string firstClass = "";
            foreach ( Example ex in examples)
            {
                if (firstClass == "")
                {
                    firstClass = ex.ExmpName;
                }
                if (ex.ExmpName != firstClass)
                {
                    firstClass = "";
                    break;
                }

            }
            if (firstClass != "")
            {
                N.Name = firstClass;
                return N;
            }
            //IF atts is empty THEN RETURN N labelled with modal example
            if (attributes.Count == 0)
            {
                return N;
            }
            //Select an attribute A according to some heuristic function (choose_best_attr)
            //ii.Generate a new node DT with A as test
            //iii.For each Value vi of A
            //(a) Let Si = all examples in S with A = vi (Take all the examples in the original set that has the attribute A)
            //(b) Use ID3 to construct a decision tree DTi for example set Si
            //(c) Generate an edge that connects DT and DT
        }
    }
    class Node
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ConnectedBy { get; set; }
        public List<Node> ConnectedWith { get; set; }
    }
    class Attribute
    {
        public int AttrID { get; set; }
        public string AttrName { get; set; }
        public double informationGain { get; set; }
        public List<AttributeClass> classes { get; set; }
    }
    class AttributeClass
    {
        public AttributeClass(string name)
        {
            C_Name = name;
            IsSelected = false;
        }
        public string C_Name { get; set; }
        public bool IsSelected { get; set; }
    }
    class Example
    {
        public Example (string attributeLists, string Attrs)
        {
            //The user should provide the list of attributes that he wants to work with
            //But in case of giving no attributes the program will give him all the existing attributes
            if (attributeLists == "" || attributeLists == "*" || attributeLists == "all")
            {
                attributes = Program.ProgramAttributes;
            }
            //If he provided a semi-colon separated list of attributes then the program will set them
            else
            {
                try
                {
                    //Split the names of the provided list
                    string[] NeededAttributeNames = attributeLists.Split(';');
                    //Add each attribute that has a compatible name with the names provided by the user to create the example
                    foreach (string s in NeededAttributeNames)
                    {
                        attributes.Add(Program.ProgramAttributes.Where(a => a.AttrName == s).SingleOrDefault());
                    }
                }
                //In case of having an error in the names of the attributes then the program will provide the user with all attributes in the program
                catch (Exception ex)
                {
                    attributes = Program.ProgramAttributes;
                }
            }
            //Now, what are the attributes settings for this example, what is the selected value for each attribute among the classes or the values that
            //is enumerated within the attribute
            //The list of attributes should be entered like the following:
            //Humidity:dry;Sky:overcast,Temprature:warm;wind:windy;
            string[] attrsList = Attrs.Split(';');
            foreach (string attr in attrsList)
            {
                //Get the attribute name and value from the list that's entered by the user
                string AttributeName = attr.Split(':')[0];
                string AttributeValue = attr.Split(':')[1];
                foreach (Attribute A in attributes)
                {
                    //Set the selected attribute-class to true
                    if (A.AttrName  == AttributeName)
                    {
                        //Set the selected values inside each attribute in the example attributes list
                        A.classes.Find(a => a.C_Name == AttributeValue).IsSelected = true;
                    }
                }
            }
        }
        public string ExmpName { get; set; }
        public List<Attribute> attributes { get; set; }
    }
}
