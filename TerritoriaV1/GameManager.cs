using Godot;
using System;
using TerritoriaV1;

public partial class GameManager : Node2D
{
	private VillageManager villageManager;
	EvolutionOfVillage evolutionOfVillage;

	turnNB turn;

	int nbMaxTurn = 25;
	int currentTurnNb = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		turn = GetNode<turnNB>("t");
		turn.updateCurrentTurn(1);
	
		MissingRessource missingResource = GetNode<MissingRessource>("MissingRessource");
		var printer = GetNode<Printer>("Printer");
		printer.setMessageWindow(missingResource);	
		
		evolutionOfVillage = new EvolutionOfVillage(this);
		if(evolutionOfVillage != null)
	
		villageManager = new VillageManager(GetNode<TileMap>("Map"),GetNode<Printer>("Printer"),GetNode<Trader>("Trader"),evolutionOfVillage);		
		
        var messageDialog = new MessageDialog();
		messageDialog.SetErrorMessage("bienvenue, vous êtes responsables de l'import et de l'export des ressources de notre village. nous comptons sur vous");
		AddChild(messageDialog);
		messageDialog.PopupCentered();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void nextTurn(int[] export, int[] import)
	{
		
		currentTurnNb++;
		turn.updateCurrentTurn(currentTurnNb);

		if(currentTurnNb > nbMaxTurn)
		{
			EndGame(); return;
		}
		
		villageManager.NextTurn(export, import);
	}

	public void updateGraphics()
	{
		
	}

	public void EndGame()
	{
		var messageDialog = new MessageDialog();
		messageDialog.SetErrorMessage("You have lost.");
		AddChild(messageDialog);
		messageDialog.PopupCentered();
		GetTree().Quit();
	}

	public void Victory(){
		//TODO
	}

	public void _on_missing_ressource_canceled(){
		currentTurnNb--;
		turn.updateCurrentTurn(currentTurnNb);
		villageManager.applyNextTurn(false);
	}

	public void _on_missing_ressource_confirmed(){
		villageManager.applyNextTurn(true);
	}
}
