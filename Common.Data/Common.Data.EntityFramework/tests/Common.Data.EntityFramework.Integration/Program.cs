using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Data.EntityFramework;
using Jopalesha.Common.Data.EntityFramework.Integration;
using Microsoft.EntityFrameworkCore;


await using var testContext = new TestContextFactory().CreateDbContext(Array.Empty<string>());

await testContext.Database.MigrateAsync();

var repository = new TestEntityRepository(testContext);
var unitOfWork = new UnitOfWork(testContext);
var cts = CancellationToken.None;

var entity = new TestEntity("test");
await repository.AddAsync(entity, cts);
await unitOfWork.SaveAsync(cts);
Console.WriteLine($"Entity {entity} added");

entity.UpdateValue("newValue");
await unitOfWork.SaveAsync(cts);
Console.WriteLine($"Entity {entity} updated");

var updatedEntity = await repository.SingleAsync(it => it.Value == "newValue", cts);
repository.Remove(updatedEntity);
await unitOfWork.SaveAsync(cts);
Check.False(await repository.ExistsAsync(updatedEntity.Id, cts));

Console.WriteLine($"Entity {entity} removed");

Console.ReadKey();
