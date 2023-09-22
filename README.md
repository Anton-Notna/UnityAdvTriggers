# UnityAdvTriggers
3d customizable triggers. Applicable to mobs "eyes".

# Code
At the heart of everything is an abstract class `Trigger` with a single method `Contains`. It can be used something like this:
```csharp
Trigger trigger;
Transform enemy;
bool visible = trigger.Contains(enemy.position);
```
# Editor
There are several classes inherited from the `Trigger`. For example, `SectorTrigger`:

![image](https://github.com/Anton-Notna/UnityAdvTriggers/assets/68640302/090de27e-684b-4087-a3c4-0747e0d902c4)

`SectorTrigger` in Scene Window:

https://github.com/Anton-Notna/UnityAdvTriggers/assets/68640302/ce8726d4-6c50-4a24-a774-725fa1b11f04
