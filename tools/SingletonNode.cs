using Godot;
using System;

public class SingletonNode<T> : Node where T : class//represents autoload singleton in godot.
{
	private static T _instance = null;

	public static T Instance
	{

	get
	{
		if (_instance is T singleton)
			return singleton;

		throw new Exception
		{
			Source =
				$"Singleton {_instance} is null or not a type {nameof(T)}"
		};
	}
	}

	public override void _EnterTree()
	{
		_instance = this as T;
		base._EnterTree();
	}
}
