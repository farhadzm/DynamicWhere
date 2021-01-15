# DynamicQuery

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

var people = PeopleDataGenerator.GetPeople().DynamicWhere(dyn).ToList();
```
