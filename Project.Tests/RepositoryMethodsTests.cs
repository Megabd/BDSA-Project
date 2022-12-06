namespace Project.Tests;
using LibGit2Sharp;
using Project;
using Project.Infrastructure;
using System.IO.Compression;

// Tests that the program prints the correct results
public class RepositoryMethodsTests {

        private readonly ProjectContext _context;
        private UserRepo archive;
    public RepositoryMethodsTests() 
    {

        //DeleteReadOnlyDirectory("C:/Users/olive/ADS/BDSA-Project/Project.Tests/extract");
       _context = new ProjectContext();

       
            //System.IO.Directory.GetParent();

           /* string zipPath = "C:/Users/olive/ADS/BDSA-Project/Project.Tests/assignment-02.zip";
            string extractPath = "C:/Users/olive/ADS/BDSA-Project/Project.Tests/extract";

            //deploymentItemAttribute

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Read))
            {
                archive.ExtractToDirectory(extractPath);
            }*/
            

             archive = new UserRepo("C:/Users/olive/ADS/assignment-02");

             
    }

    [Fact]
    public void Test_Zip_loc () {
        archive.Name.Should().Be("assignment-02");
    }


    [Fact]

    public void Test_Commit_Frequency () {
        var test = RepositoryMethods.CommitFrequency(archive, _context);
        var testResult = new Dictionary<string, int>();
        testResult.Add("16-09-2021", 2);
        testResult.Add("17-09-2021", 5);
        testResult.Add("14-09-2022", 1);
        testResult.Add("15-09-2022", 2);
        testResult.Add("16-09-2022", 3);
        testResult.Add("20-09-2022", 6);
        testResult.Add("21-09-2022", 2);
        testResult.Add("22-09-2022", 3);
        test.Should().BeEquivalentTo(testResult);

        //DeleteReadOnlyDirectory("C:/Users/olive/ADS/BDSA-Project/Project.Tests/extract");
    }


    [Fact]
    public void Test_Commit_Author() {
        /*var test = RepositoryMethods.CommitAuthor(archive, _context);
        var innerDic = new Dictionary<string, int>();
        var innerDic1 = new Dictionary<string, int>();
        var innerDic2 = new Dictionary<string, int>();
        var innerDic3 = new Dictionary<string, int>();
        var innerDic4 = new Dictionary<string, int>();
        var innerDic5 = new Dictionary<string, int>();

        innerDic.Add("15-09-2022", 1);
        innerDic.Add("16-09-2022", 2);
        innerDic1.Add("22-09-2022", 1);
        innerDic2.Add("20-09-2022", 3);
        innerDic2.Add("21-09-2022", 2);
        innerDic3.Add("17-09-2021", 5);
        innerDic4.Add("16-09-2021", 2);
        innerDic4.Add("14-09-2022", 1);
        innerDic4.Add("15-09-2022", 1);
        innerDic4.Add("16-09-2022", 1);
        innerDic5.Add("20-09-2022", 3);
        innerDic5.Add("22-09-2022", 2);
        
        var testResult = new Dictionary<string, Dictionary<string, int>> {
            {" name = HelgeCPH ", innerDic},
            {" name = Lignio ", innerDic1},
            {" name = Oliver Flyckt Wilhjelm ", innerDic2},
            {" name = Paolo Tell ", innerDic3},
            {" name = Rasmus Lystrøm ", innerDic4},
            {" name = tcla ", innerDic5}
        };
        /*testResult.Add("name = HelgeCPH", innerDic);
        testResult.Add("name = Lignio", innerDic1);
        testResult.Add("name = Oliver Flyckt Wilhjelm", innerDic2);
        testResult.Add("name = Paolo Tell", innerDic3);
        testResult.Add("name = Rasmus Lystrøm", innerDic4);
        testResult.Add("name = tcla", innerDic5);*/
        /*
        test.Should().Equal(testResult);*/
    } 

    
 public static void DeleteReadOnlyDirectory(string directory)
{
    foreach (var subdirectory in Directory.EnumerateDirectories(directory)) 
    {
        DeleteReadOnlyDirectory(subdirectory);
    }
    foreach (var fileName in Directory.EnumerateFiles(directory))
    {
        var fileInfo = new FileInfo(fileName);
        fileInfo.Attributes = FileAttributes.Normal;
        fileInfo.Delete();
    }
    Directory.Delete(directory);
}
    
    }