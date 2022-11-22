namespace Project.Frontend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




    public class PretendAuthorService
    {
        List<PretendAuthor> PretendAuthors = new List<PretendAuthor>()
        {
            new PretendAuthor(){aName="Benjamin",aDate=new DateTime(2007, 07, 12, 06, 32, 00)},
            new PretendAuthor(){aName="Benjamin",aDate=new DateTime(2007, 07, 12, 07, 32, 00)},
            new PretendAuthor(){aName="Benjamin",aDate=new DateTime(2008, 10, 12, 06, 32, 00)},
            new PretendAuthor(){aName="Nicholas",aDate=new DateTime(2007, 07, 12, 06, 32, 00)},
            new PretendAuthor(){aName="Nicholas",aDate=new DateTime(2008, 12, 12, 06, 32, 00)},
            new PretendAuthor(){aName="Nicholas",aDate=new DateTime(2009, 12, 15, 08, 32, 00)},
            new PretendAuthor(){aName="Oliver",aDate=new DateTime(2006, 04, 16, 09, 32, 00)},
            new PretendAuthor(){aName="Oliver",aDate=new DateTime(2007, 12, 15, 08, 32, 00)},
            new PretendAuthor(){aName="Oliver",aDate=new DateTime(2007, 12, 15, 08, 32, 00)},
            new PretendAuthor(){aName="Oliver",aDate=new DateTime(2008, 06, 07, 12, 34, 00)}
        };
        public async Task<List<PretendAuthor>> PretendAuthorList() 
        {
            return await Task.FromResult(PretendAuthors);
        }
    }
