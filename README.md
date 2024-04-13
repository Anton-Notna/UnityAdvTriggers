# UnityAdvTriggers
Customizable triggers for 3D space. Applicable to mobs "eyes".

# Installation
UnityAdvTriggers is the upm package, so the install is similar to other upm packages:
1. Open `Window/PackageManager`
2. Click `+` in the right corner and select `Add package from git url...`
3. Paste link to this package `https://github.com/Anton-Notna/UnityAdvTriggers.git` and click `Add`

# Usage
At the heart of everything is an abstract class `Trigger` with a single method `Contains`. There is an expamle of usage:
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
