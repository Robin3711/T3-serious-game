using System;
using Godot;
using Godot.Collections;

namespace TerritoriaV1;
//Faire que pour chaque tour il faux retirer des matériaux en fonction de se que l'on veux construire
public class PlaceableFactory
{
    public Placeable CreateHouse()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] needCapacities = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.WOOD] = 1;
        output[(int)ResourceType.MONEY] = 2;
        //needCapacities[(int)ResourceType.WOOD] = 20;
        Placeable house = new Placeable(PlaceableType.HOUSE,input, output,20,needCapacities);
        return house;
    }

    public Placeable CreateSawmill()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] needCapacities = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 2;
        output[(int)ResourceType.WOOD] = 2;
        //needCapacities[(int)ResourceType.MONEY] = 100;
        Placeable sawmill = new Placeable(PlaceableType.SAWMILL,input, output,50, needCapacities);
        return sawmill;
    }

    public Placeable CreateTrainStation()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] needCapacities = new int[Enum.GetNames(typeof(ResourceType)).Length];
        Placeable trainStation = new Placeable(PlaceableType.TRAIN_STATION,input, output,0, needCapacities);
        return trainStation;   
    }

    public Placeable CreateBar()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] needCapacities = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.BEER] = 1;
        output[(int)ResourceType.MONEY] = 3;
        //needCapacities[(int)ResourceType.BEER] = 200;
        Placeable sawmill = new Placeable(PlaceableType.BAR,input, output,150,needCapacities);
        return sawmill;
    }

    public Placeable CreateField()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] needCapacities = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 2;
        output[(int)ResourceType.HOP] = 3;
        //needCapacities[(int)ResourceType.MONEY] = 110;
        Placeable field = new Placeable(PlaceableType.FIELD,input, output,50, needCapacities);
        return field;
    }

    public Placeable CreateIceUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] needCapacities = new int[Enum.GetNames(typeof(ResourceType)).Length];
        output[(int)ResourceType.ICE] = 5;
        //needCapacities[(int)ResourceType.WOOD] = 20;
        Placeable ice_usine = new Placeable(PlaceableType.ICE_USINE,input, output,50, needCapacities);
        return ice_usine;
    }

    public Placeable CreateBeerUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] needCapacities = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.ICE] = 4;
        input[(int)ResourceType.HOP] = 5;
        output[(int)ResourceType.BEER] = 4;
        //needCapacities[(int)ResourceType.ICE] = 100;
        //needCapacities[(int)ResourceType.HOP] = 250;
        Placeable beer_usine = new Placeable(PlaceableType.BEER_USINE,input, output,50, needCapacities);
        return beer_usine;
    }

    /*public void Destroy(Placeable[,] placeables, PlaceableType type)
    {
        for (int i = 0; i < placeables.GetLength(0); i++)
        {
            for (int j = 0; j < placeables.GetLength(1); j++)
            {
                if (placeables[i,j] != null && placeables[i, j].getPlaceableType() == type)
                {
                    placeables[i, j] = null;
                    return;
                }
            }
        }
    }*/
}
