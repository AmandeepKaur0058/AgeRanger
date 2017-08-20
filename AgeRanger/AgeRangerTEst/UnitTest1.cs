using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AgeRanger.Data;
using AgeRanger.Models;
using AgeRanger.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AgeRangerTEst
{
    [TestClass]
    public class AgeRangerTests
    {
        [TestMethod]
        public void TestToEnsureNothingIsReturnedWhenAgeGroupDoesNotMatch()
        {
            var dbContextMock = new Mock<IApplicationDbContext>();
            var personList = new List<Person> {new Person {Id = 1, FirstName = "aman", LastName = "", Age = 20}};
            var ageGroupList = new List<AgeGroup>();

            var personDbSet = GetMockDbSet<Person>(personList);            
            var agrGroupDbSet = GetMockDbSet<AgeGroup>(ageGroupList);

            dbContextMock.Setup(m => m.People).Returns(personDbSet);
            dbContextMock.Setup(m => m.AgeGroups).Returns(agrGroupDbSet);

            PersonRepository repository = new PersonRepository(dbContextMock.Object);
            var result = repository.GetPersons();

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void ReturnNothingIfIdIsNotExist()
        {
            var dbContextMock = new Mock<IApplicationDbContext>();
            var personList = new List<Person> { new Person { Id = 1, FirstName = "aman", LastName = "", Age = 20 } };
        
            var personDbSet = GetMockDbSet<Person>(personList);
            dbContextMock.Setup(m => m.People).Returns(personDbSet);

            PersonRepository repository = new PersonRepository(dbContextMock.Object);            
            var temp = repository.GetPersonById(100);

            Assert.IsNull(temp);
        }

        [TestMethod]
        public void ReturnNothingIfQueryDoesNotMatch()
        {
            var dbContextMock=new Mock<IApplicationDbContext>();
            var personList=new List<Person>{new Person{ Id = 1, FirstName = "aman", LastName = "", Age = 20,AgeGroup = "Young"} };

            var personDbSet = GetMockDbSet<Person>(personList);
            dbContextMock.Setup(m => m.People).Returns(personDbSet);

            PersonRepository repository=new PersonRepository(dbContextMock.Object);
       
            var temp = repository.GetPersonBySearch("AmandeepKaur",personList);

            Assert.IsFalse(temp.Any());

        }
        private DbSet<T> GetMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }
    }
}
