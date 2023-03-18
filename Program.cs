using Pipeline;

internal class Program
{
	static void Main(string[] args) {

		//implementing the pipeline to process the steps of a DnD5e attack as a showcase
		//the pipeline allows the developer to define custom processes so any multi step process can be defined
		Pipeline<ActionData> attackPipeline = new Pipeline<ActionData>();
		//get user input
		attackPipeline.AddProcess((ActionData input, Func<ActionData> next) => {
			
			Console.WriteLine("Enter the name of a target you wish to attack");
			input.Target = Console.ReadLine();
			input.Target = String.IsNullOrEmpty(input.Target) ? "Default Target" : input.Target;

			Console.WriteLine("Enter the name of your weapon");
			input.Weapon = Console.ReadLine();
			input.Weapon = String.IsNullOrEmpty(input.Weapon) ? "Default Weapon" : input.Weapon;

			input.HitModifier = 2;
			input.TargetAC = 12;
			input.WeaponDamageDie = 10;

			return next();
		});

		//attack roll
		attackPipeline.AddProcess((ActionData input,Func<ActionData> next) => {
			Console.WriteLine("Rolling to hit!");
			input.AttackRollResult = input.AttackRoll(input.HitModifier);
			input.DidHit = input.didHit(input.AttackRollResult,input.TargetAC) ? "Hit" : "Miss";
			if (input.DidHit == "Miss") {
				Console.WriteLine($"Attack missed. Rolled {input.AttackRollResult}");
				return input;
			}
			Console.WriteLine($"Attack hit! Rolled {input.AttackRollResult}.");
			return next();
		});

		//damage roll
		attackPipeline.AddProcess((ActionData input,Func<ActionData> next) => {
			Console.WriteLine("Rolling for damage!");
			input.DamageDealt = input.WeaponDamage(input.WeaponDamageDie);
			Console.WriteLine($"{input.DamageDealt} dealt to {input.Target} with your trusy {input.Weapon}");
			return input;
		});

		while (true) {
			Console.WriteLine(attackPipeline.Execute(ActionData.CreateAttackData()));
		}
	}

	class ActionData {
		public static ActionData CreateAttackData() {
			return new ActionData();
		}
		public ActionData() {
			Target = "";
			Weapon = "";
			HitModifier = 0;
			TargetAC = 10;
			WeaponDamageDie = 4;
			DamageDealt = 0;
			AttackRollResult = 0;
			DidHit = "";
		}

		public String Target { get; set; }
		public String Weapon { get; set; }
		public int HitModifier { get; set; }
		public int TargetAC { get; set; }
		public int WeaponDamageDie { get; set; }
		public int DamageDealt { get; set; }
		public int AttackRollResult { get; set; }
		public string DidHit { get; set; }
		public bool didHit(int result,int ac) {
			return result >= ac;
		}
		public int WeaponDamage(int die) {
			return new Random().Next(die);
		}
		public int AttackRoll(int bonus) {
			return new Random().Next(20) + bonus;
		}
		public override string ToString() {
			return $"Target: {Target}. Weapon: {Weapon}. Hit modifier: {HitModifier}. Target armour class: {TargetAC}. Weapon damage: {WeaponDamageDie}. Damage dealth: {DamageDealt}. Attack result: {DidHit}. Attack roll: {AttackRollResult}";
		}
	}
}