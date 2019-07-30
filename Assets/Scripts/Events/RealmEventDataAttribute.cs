using System;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
internal sealed class RealmEventDataAttribute : Attribute
{
    public RealmEventDataAttribute(string eventType) {
        this.eventType = eventType;
    }

    public string eventType {
        get; set;
    }
}
