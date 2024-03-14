// See https://aka.ms/new-console-template for more information
using Loops;

Console.WriteLine("Hello, World!");


// this is a variable for name
string name = "Bob";
string lastName = "Smith"; // this is a variable for last name


string thePlaceILoveToGo = "Home";
// the variable above is a place i like to go

string livesacathas = "Cat";
string IMAVARIABLE = "Cat";

string my9Lives = "Cat";
string myNineLives = "Cat";

string Name = "Jim";

var Class = "Cat";

// Comments:
// RTFM - Read The F**** Message

/*sdfsdf
  sdffsdfs
  
  dfsdfsdf
  sdfsdfsdf
  sdfsdf
 */

/*

int age = 0;

age = 25;

//age = "30";

Console.WriteLine("My age is: " + age);

byte myAge = 30;
byte numberOfWorkers = 50;

sbyte x = -10;

long myAccount = 100000000000;


double payPerHour = 16.5;

bool isHappy = true;
bool isSad = false;

//NO!
//bool isCat = 0;

char l = 'c';
char heb = 'א';
char zero = '0';
string word = "abcd";

const double pi = 3.14;
//pi = 3.1415;

myAge = 31;
Console.WriteLine("pi = " + pi);

int a = 5, b = 10;


string myName = "Bob";

string someValue = "";
string someOtherValue = string.Empty;

Console.WriteLine("Please enter your name:" + someValue);



int a2 = 55;
int b2 = 10;

//Console.WriteLine(a2 + b2);

int c2 = a2 + b2;
Console.WriteLine(a2 + "+" + b2 + "=" + c2);
c2 = a2 - b2;
Console.WriteLine(a2 + "-" + b2 + "=" + c2);
c2 = a2 * b2;
Console.WriteLine(a2 + "*" + b2 + "=" + c2);
c2 = a2 / b2;
Console.WriteLine(a2 + "/" + b2 + "=" + c2);

double d1 = 55;
double d2 = 10;
double d3 = d1 / d2;
Console.WriteLine(d1 + "/" + d2 + "=" + d3);

int i = 10;
Console.WriteLine(i);
i++;
i++;
i++;
i++;
i++;
Console.WriteLine(i);
i--;
Console.WriteLine(i);

int z = 10;
z += 5;
Console.WriteLine(z);
z -= 3;
Console.WriteLine(z);


int val1 = int.MaxValue;
val1 += 1000000;
Console.WriteLine(val1);

byte b1 = 10;
Console.WriteLine(b1);
b1 += 200;
Console.WriteLine(b1);
b1 += 200;
Console.WriteLine(b1);
b1 += 200;
Console.WriteLine(b1);
b1 += 200;
Console.WriteLine(b1);
b1 += 200;
Console.WriteLine(b1);
b1 += 200;
Console.WriteLine(b1);


double hourlyRate = 30.7;
int hoursPerDay = 8;
int daysPerMonth = 25;

double monthlySalary = hourlyRate * hoursPerDay * daysPerMonth;
Console.WriteLine(monthlySalary);

string s1 = "Hello";
string s2 = "World";
string s3 = s1 + " " + s2;

Console.WriteLine("This \n is \n multiline");

bool isWhiteSpace = char.IsWhiteSpace('\n');
bool isDigit = char.IsDigit('6');
Console.WriteLine("isDigit:" + isDigit);
bool isLetter = char.IsLetter('Z');
Console.WriteLine("isLetter:" + isLetter);


DateTime born = new(1989, 10, 1, 14, 20, 13);
DateTime died = born.AddDays(100);
died = died.AddMinutes(100);
Console.WriteLine(born);
Console.WriteLine(died.ToString("yyyy-dd-MM HH:mm"));

Console.WriteLine(born.ToShortTimeString());

DateTime now = DateTime.Now;

int intnumber = 23512;

long longnumber = intnumber;

Console.WriteLine(longnumber);

int myage = 54;

int minage = 18;

int maxage = 70;

bool isAllowedIn = (myage > minage) && (myage < maxage);
Console.WriteLine(isAllowedIn);

string name5 = "bomba";

switch (name5)
{
    case "gar":
        Console.WriteLine("gar gar");
        break;
    case "bomba":
        Console.WriteLine("bomba it is");
        break;
}
int i2 = 0;
do
{
    i2++;
    Console.WriteLine(i2);

}  while (i2 < 10);

int i3 = 0;

while (i3 < 10)
{
    i3++;
    Console.WriteLine("i3 is now: " + i3);
}

string input2;
int selection2;

do
{
    Console.WriteLine("select an option:");
    Console.WriteLine("1: add apples");
    Console.WriteLine("2: add cars");
    Console.WriteLine("3: add furries");
    Console.WriteLine("99: Exit");
    bool parsedOK;
    do
    {
        Console.WriteLine("enter a numaric value");
        input2 = Console.ReadLine();
        parsedOK = int.TryParse(input2, out selection2);
        if (!parsedOK) Console.WriteLine("invalid value");
    } while (!parsedOK);

    switch (selection2)
    {
        case 1:
            Console.WriteLine("added apples");
            break;
        case 2:
            Console.WriteLine("added cars");
            break;
        case 3:
            Console.WriteLine("added furries OwO");
            break;
        case 99:
            Console.WriteLine("Exit");
            break;
        default:
            Console.WriteLine("unknown comand");
            break;
    }
} while (selection2 < 99);
int rowsAndColoms;
string userInput;
Console.WriteLine("choose the number of rows and coloms to print");
rowsAndColoms = int.Parse(Console.ReadLine());

int rows = 1;

while (rows <= rowsAndColoms)
{
    int col = 1;
    while (col <= rowsAndColoms)
    {
        int resault = col * rows;
        Console.Write(resault + "\t");
        col++;
    }
    Console.WriteLine();
    rows++;
}


int s;

for (int i4 = 0; i4 < 10; i4++)
{
    Console.WriteLine("your i4 is: " + i4);
}


// if devided by 3 print triple
// if divided by 4 print qwadruple
// if divided by 3 and 4 print ectuple 
// if divided by 8 print sexulte 

decimal afterFactory = 0;

int currentNumber = 1;

int firstNumber = 3;
int secondNumber = 4;
int thirdNumber = 7;

while (currentNumber < 100)
{
    if (currentNumber % firstNumber == 0 & currentNumber % secondNumber == 0 & currentNumber % thirdNumber == 0) Console.WriteLine(currentNumber + " is all");
    else if (currentNumber % firstNumber == 0 & currentNumber % secondNumber == 0) Console.WriteLine(currentNumber + " is ectuple");
    else if (currentNumber % firstNumber == 0) Console.WriteLine(currentNumber + " is triple");
    else if (currentNumber % secondNumber == 0) Console.WriteLine(currentNumber + " is qwadruple");
    else if (currentNumber % thirdNumber == 0) Console.WriteLine(currentNumber + " is sexulte");

    currentNumber++;
}

int numberOfBook = 6;

int CalculateBookPrice(int bookNum)
{
    int books = bookNum;

    int result = 0;
    while (books > 0)
    {
        result += 10; 
        books--;
    }
    return result;
}

decimal ApplyDiscount(decimal originalPrice, decimal discountPercentage = 0.05M)
{
    decimal discounted = originalPrice - (originalPrice * discountPercentage);

    return discounted;
}
void PrintSummary(int numberOfBooks)
{
    decimal result = 0;
    decimal originalResult = CalculateBookPrice(numberOfBooks);
    result = ApplyDiscount(originalResult);
    decimal discount = originalResult - result;

    Console.WriteLine("the number of books are: " + numberOfBook + "the original price is: " + originalResult + "the discount applied is: " + discount + "the final price is: " + result);
    
};

PrintSummary(numberOfBook);





void DisplayProduct(string name, int price, int quantity)
{

    Console.WriteLine("The product name is: " + name + "and its price is: " + price);
    Console.WriteLine("There are " + quantity + "of" + name + "the total price is: " + quantity * price);
}

DisplayProduct(name: "harish",quantity: 3, price: 12);
*/



new BankAccount();