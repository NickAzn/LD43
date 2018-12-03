using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Building {

    bool HireWorker(Person p);

    void RemoveWorker(Person p);

    void Upgrade();

    Sprite GetWorkerSprite();

    List<Person> GetWorkerList();

    int GetMaxWorkers();

    string GetName();

    int GetBuildingPower();
}
