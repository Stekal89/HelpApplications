namespace MyFirstBlazorApp.Client.Models
{
    /*
        Expressions that evaluate to complex types
        Complex types can also be passed as a parameter value to both HTML attributes and also Blazor components' [Parameter] properties. 
        When passing a non-simple value as an expression to an HTML attribute, Blazor will render the value using ValuePassed.ToString(); 
        when the value is being passed to a [Parameter] property on a Blazor component, the object itself is passed.

        Take the following Person class as an example:
     */
    public class Person
    {
        public string Salutation { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }

        public override string ToString() => $"{Salutation} {GivenName} {FamilyName}";
    }
}
