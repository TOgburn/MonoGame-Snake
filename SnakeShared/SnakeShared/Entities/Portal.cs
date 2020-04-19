using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoEngine;

namespace Snake.Entities
{
	public class Portal : Entity
	{
		public const int Size = 30;
		public const int AnimationSpeed = 250;
		public readonly ColliderRectangle MainCollider;
		private readonly ReadOnlyDictionary<Directions, int> DestinationIDs;
		private readonly ReadOnlyDictionary<Directions, bool> ReverseDirection;

		public Portal(int x, int y, int id, int goto_up, int goto_down, int goto_left, int goto_right, ReadOnlyDictionary<Directions, bool> reverse_direction = null) {
			this.Depth = 100;
			this.Position = new Vector2(x, y);
			this.ID = id;
			this.DestinationIDs = new ReadOnlyDictionary<Directions, int>(new Dictionary<Directions, int> {
				[Directions.Up] = goto_up,
				[Directions.Down] = goto_down,
				[Directions.Left] = goto_left,
				[Directions.Right] = goto_right
			});
			this.ReverseDirection = reverse_direction;

			var regions = new Region[] {
				new Region(ContentHolder.Get(AvailableTextures.portal_0)),
				new Region(ContentHolder.Get(AvailableTextures.portal_1)),
			};
			var sprite = new AnimatedSprite(regions, Portal.AnimationSpeed);
			this.AddSprite("main", sprite);
			this.MainCollider = this.AddColliderRectangle("main", 0, 0, regions[0].GetWidth(), regions[0].GetHeight());
		}

		public Portal(PortalSpawn spawn) : this(spawn.X, spawn.Y, spawn.ID, spawn.GotoUp, spawn.GotoDown, spawn.GotoLeft, spawn.GotoRight, spawn.ReverseDirection) {}

		public Portal GetDestination(Directions direction) => Engine.GetAllInstances<Portal>().First(portal => portal.ID == this.DestinationIDs[direction]);
	}
}
