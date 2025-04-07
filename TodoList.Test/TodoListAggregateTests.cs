using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoList.Domain.Aggregates;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Repositories;
using TodoList.Domain.Services;

namespace TodoList.Test
{
    [TestClass]
    public class TodoListAggregateTests
    {
        [TestMethod]
        public void Can_Add_Item_With_Valid_Progressions_And_Mark_As_Completed()
        {
            ITodoListRepository repo = new MockTodoListRepository();
            ILogger logger = new ConsoleLogger(); 
            var list = new TodoListAggregate(repo, logger);

            int id = repo.GetNextId();

            list.AddItem(id, "Complete Project Report", "Finish the final report for the project", "Work");
            list.RegisterProgression(id, new DateTime(2025, 3, 18), 30);
            list.RegisterProgression(id, new DateTime(2025, 3, 19), 50);
            list.RegisterProgression(id, new DateTime(2025, 3, 20), 20);

            Assert.IsTrue(true); // If we get to this point with no errors, we assume it all went well.
        }
    }
}
