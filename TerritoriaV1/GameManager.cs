using Godot;
using System;
using System.Security.Principal;
using TerritoriaV1;

public partial class GameManager : Node2D
{
	private VillageManager villageManager;
	private EvolutionOfVillage evolutionOfVillage;

	turnNB turn;
	int nbMaxTurn = 50;
	int currentTurnNb = 1;

	private Printer print;
	private Trader trade;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		turn = GetNode<turnNB>("t");
		turn.updateCurrentTurn(1);
		turn.Visible = false;
	
		MissingRessource missingResource = GetNode<MissingRessource>("MissingRessource");
		var printer = GetNode<Printer>("Printer");
		printer.setMessageWindow(missingResource);	

		var trader = GetNode<Trader>("Trader");
		
		evolutionOfVillage = new EvolutionOfVillage(this);
		if(evolutionOfVillage != null)
	
		villageManager = new VillageManager(GetNode<TileMap>("Map"),printer,trader,evolutionOfVillage);	

		this.print = printer;	
		this.trade = trader;
	}
	
	public void nextTurn(int[] export, int[] import, int[] money)
	{
		
		currentTurnNb++;
		turn.updateCurrentTurn(currentTurnNb);

		if(currentTurnNb > nbMaxTurn)
		{
			EndGame(); return;
		}
		
		villageManager.NextTurn(export, import, money);
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
		GetTree().ReloadCurrentScene();
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

	public void printMessage(string message)
	{
		var messageDialog = new MessageDialog();
		messageDialog.SetErrorMessage(message);
		AddChild(messageDialog);
		messageDialog.PopupCentered();
	}

	public void _on_start_pressed(){
		var menu = GetNode<TextureRect>("StartMenu");
		this.trade.setVisibility();
		this.print.setVisibility();
		turn.Visible = true;
		menu.Visible = false;
		printMessage("bienvenue, vous êtes responsables de l'import et de l'export des ressources de notre village. nous comptons sur vous");
	}

	public void _on_exit_pressed(){
		GetTree().Quit();
	}
}
