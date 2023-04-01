using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipamentManager : MonoBehaviour
{
    #region  Singleton
    public static EquipamentManager instance;
    
    void Awake()
    {
        instance = this;
    }
    #endregion


    Equipament[] currentEquips;
    public Equipament[] defaultEquipament;
    SkinnedMeshRenderer[] currentMeshes;
    public SkinnedMeshRenderer targetMesh;
    public delegate void OnEquipamentChange(Equipament newItem, Equipament OldItem);
    public OnEquipamentChange OnEquipamentChanged;
    Inventory inventory;
    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipamentSlot)).Length;
        currentEquips = new Equipament[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        EquipDefault();
    }

    public void Equip (Equipament newItem){
        int slotIndex = (int)newItem.equipSlot;
        Equipament oldItem = Unequip(slotIndex);;

        if(OnEquipamentChanged != null){
            OnEquipamentChanged.Invoke(newItem, oldItem);
        }

        currentEquips[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        currentMeshes[slotIndex] = newMesh;
    }

    public Equipament Unequip(int slotIndex){

        if(currentEquips[slotIndex] != null){
            if(currentMeshes[slotIndex] != null){
                Destroy(currentMeshes[slotIndex].gameObject);
            }
        Equipament oldItem = currentEquips[slotIndex];
        inventory.Add(oldItem);
        currentEquips[slotIndex] = null;

        if(OnEquipamentChanged != null){
            OnEquipamentChanged.Invoke(null, oldItem);
        }
        return oldItem;
        }
        return null;
    }

    public void UnequipAll(){
        for(int i = 0; i < currentEquips.Length; i++){
            Unequip(i);
        }
        EquipDefault();
    }

    void EquipDefault(){
        foreach(Equipament item in defaultEquipament){
            Equip(item);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            UnequipAll();
        }
    }
}
