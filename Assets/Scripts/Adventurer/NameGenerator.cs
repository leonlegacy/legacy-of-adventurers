public class NameGenerator
{
    string[] firstNames = new string[100]
    {
        "Liam", "Noah", "Oliver", "Elijah", "James", "William", "Benjamin", "Lucas", "Henry", "Alexander",
        "Mason", "Michael", "Ethan", "Daniel", "Jacob", "Logan", "Jackson", "Levi", "Sebastian", "Mateo",
        "Jack", "Owen", "Theodore", "Aiden", "Samuel", "David", "Joseph", "Carter", "Wyatt", "John",
        "Matthew", "Luke", "Grayson", "Isaac", "Gabriel", "Julian", "Leo", "Hudson", "Anthony", "Dylan",
        "Ezra", "Thomas", "Charles", "Christopher", "Jaxon", "Cameron", "Maverick", "Ryan", "Zachary", "Nolan",
        "Eli", "Jameson", "Logan", "Jackson", "Miles", "Connor", "Aaron", "Adam", "Evan", "Jesse",
        "Austin", "Mason", "Andrew", "Elliot", "Jordan", "Brayden", "Nathan", "Caleb", "Hunter", "Christian",
        "Xander", "Giovanni", "Josiah", "Isaiah", "Jaxon", "Robert", "Lincoln", "Thomas", "Ezekiel", "Landon",
        "Elliot", "Kai", "Zayden", "Asher", "Colton", "Derek", "Gavin", "Chase", "Milo", "Bryson",
        "Brody", "Nolan", "Riley", "Harrison", "Nico", "Weston", "Jace", "Karter", "Sawyer", "Jasper"
    };

    string[] lastNames = new string[100]
    {
        "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
        "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
        "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "King", "Wright",
        "Scott", "Torres", "Nguyen", "Hill", "Adams", "Baker", "Nelson", "Carter", "Mitchell", "Perez",
        "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez",
        "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey", "Rivera", "Cooper",
        "Richardson", "Cox", "Howard", "Ward", "Flores", "James", "Butler", "Simmons", "Foster", "Gonzalez",
        "Bryant", "Alexander", "Russell", "Griffin", "Diaz", "Hayes", "Myers", "Long", "Ward", "Jenkins",
        "Barnes", "Hughes", "Price", "Stone", "Hunter", "Patel", "Sullivan", "Woods", "Washington", "Kennedy",
        "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez"
    };

    public string RandomName()
    {
        string generatedName = firstNames[UnityEngine.Random.Range(0, 100)] + " " + lastNames[UnityEngine.Random.Range(0, 100)];
        return generatedName;
    }
}
