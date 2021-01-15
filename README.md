# DynamicWhere
```cs
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FamilyName { get; set; }
}
```

```cs
var dyn = new List<DynamicModel>();

dyn.Add(new DynamicModel()
{
    ComparisonMethod = ComparisonMethod.DoesNotContain,
    PropertyValue = "s",
    PropertyName = "FamilyName"
});
dyn.Add(new DynamicModel()
{
    ComparisonMethod = ComparisonMethod.Contains,
    PropertyValue = "a",
    PropertyName = "Name"
});

var people = _dbContext.Users.DynamicWhere(dyn).ToList();
```
 People variable is equal to all records that `Name` property contain `a` and `Family Name` property does not contain `s`.
