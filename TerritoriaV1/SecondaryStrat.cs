using System;
using System.Collections.Generic;
using Godot;
using TerritoriaV1;

public class SecondaryStrat : BuildingStrategy
{

    public SecondaryStrat(Placeable[,] placeables,TileType[,] tiles)
    {
        SetTiles(tiles);
    }
    override 
    public Placeable[,] BuildNewPlaceable(int[] import,
        int[] export, PlaceableFactory factory, 
        TileType[] targetTile, Placeable[,] placeables, int[] resources, int[] oldResources)
    {
        int[] resourcesNeed = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        int[] resourcesProduction = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        int beerProduction = oldResources[ResourceType.BEER.GetHashCode()] + import[ResourceType.BEER.GetHashCode()] - export[ResourceType.BEER.GetHashCode()];
        foreach (Placeable placeable in placeables)
        {
            if (placeable != null)
            {
                int[] needs = placeable.getResourceNeeds();
                int[] prod = placeable.getResourceProduction();
                for (int i = 0 ; i < resourcesNeed.Length; i++)
                {
                    resourcesNeed[i] += needs[i]+export[i];
                    resourcesProduction[i] += prod[i]+resources[i];
                }

                if (placeable.getPlaceableType() == PlaceableType.BAR)
                    beerProduction += placeable.getProduct();
            }
        }
        List<Placeable> newPlaceables = new List<Placeable>();
        // si on a plus de glace et de houblon que ce que l'on consomme
        if((resourcesProduction[ResourceType.HOP.GetHashCode()]*1.5 > resourcesNeed[(int)ResourceType.HOP]) && (resourcesProduction[(int)ResourceType.ICE]*1.5 > resourcesNeed[(int)ResourceType.ICE]))
        {
            if(resources[(int)ResourceType.WOOD] > 10)
            {
                    newPlaceables.Add(factory.CreateBeerUsine());
                    resources[(int)ResourceType.WOOD] -=10;
            }
        }

        int nbHouse = 0, nbBar = 0;
        foreach (Placeable placeable in placeables)
        {
            if (placeable!=null)
            {
                if (placeable.getPlaceableType() == PlaceableType.HOUSE)
                    nbHouse++;
                else if (placeable.getPlaceableType() == PlaceableType.BAR)
                    nbBar++;
            }
        }
        GD.Print("BeerProd : "+beerProduction+" "+nbHouse);
        while (beerProduction/10<nbHouse)
        {
            nbHouse--;
            Destroy(PlaceableType.HOUSE, placeables);
        }
        if(resourcesProduction[ResourceType.BEER.GetHashCode()]*1.25 > resourcesNeed[ResourceType.BEER.GetHashCode()]) // le joueur a interet a exporter ses bieres si il veut pas qu'on construisent des bars partout
        {
                if(resources[(int)ResourceType.WOOD] > 10 && nbBar*10<=nbHouse)
                {
                    newPlaceables.Add(factory.CreateBar());
                    resources[(int)ResourceType.WOOD] -=10;
                    nbBar++;
                }

                for (int i = 0; i < 3 && nbBar*10>nbHouse && resources[(int)ResourceType.WOOD] > 10; i++)
                {
                    newPlaceables.Add(factory.CreateHouse());
                    resources[(int)ResourceType.WOOD] -= 10;
                    nbHouse++;
                }
        }

        while (nbBar*10<nbHouse)
        {
            Destroy(PlaceableType.HOUSE,placeables);
            nbHouse--;
        }
        
        
        foreach (Placeable placeable in newPlaceables)
        {
            PlacePlaceable(placeables,placeable, targetTile[placeable.getPlaceableType().GetHashCode()]);
        }
        
        
        
        Destroy(PlaceableType.SAWMILL,placeables);
        Destroy(PlaceableType.FIELD,placeables);
        Destroy(PlaceableType.ICE_USINE,placeables);

        return placeables;
    }
    override
    public int[,] GetExchangesRates()
    {
        int[,] exchangesRates = new[,]
        {
            { 1, 2, 2, 8 }, //import
            { 1, 1, 1, 6 } //export
        };
        return exchangesRates;
    }

     override public Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile)
     {
            bool notPlaced = true;
            for (int i = 0; i < placeables.GetLength(0) && notPlaced; i++)
            {
                for (int j = 0; j < placeables.GetLength(1) && notPlaced; j++)
                {
                    if ((HasAdjacentPlaceableOfType(i, j, PlaceableType.TRAIN_STATION, placeables)||HasAdjacentPlaceableOfType(i, j, PlaceableType.RAIL, placeables)) && CanPlaceAtLocation(i, j, targetTile, placeables))
                    {
                        placeables[i, j] = placeable;
                        notPlaced = false;
                    }
                }
            }
               if (notPlaced)
                {
                    PlaceRandomly(targetTile, placeable, placeables);
                }

        return placeables;
    }
}
