For this project, I opted to implement the MVC (Model-View-Controller) pattern.

I leverage scriptable objects extensively, not only as models but also for handling events. This approach allows for high decoupling of behaviours. 

I prefer using drag-and-drop in the inspector. Because unity does not support Interfaces in the inspector, I use a RequireInterfaceAttribute to make this work. This trick is from this article ([link](https://www.patrykgalach.com/2020/01/27/assigning-interface-in-unity-inspector/))

ScriptableObjects reset on restart or when exiting play mode, with an option to disable this behaviour. 

I use **BoolMultiCompareEventBehaviour** to handle various tasks, where Unity events depend on the Boolean variable comparison results.

Now, I will provide an explanation for some scripts. Not every script is described because some are self-explanatory

-------------------------------------------------------------------------------------------------------------------------

**GameBehaviour**: orchestrates game progression by triggering events or updating models accordingly.

**GameEvent**: scriptable objects are utilized with IGameEventListener, enabling scripts to register, unregister, or raise events directly.

**GameEventBehaviour**: GameEventBehaviour accepts a GameEvent and in respone you can setup several Unity Events. 

**HoleFieldBehaviour**: randomly selects a hole from HolesListVariable to pop a mole or non-mole. The hole removes itself from the list when active and re-adds itself when inactive.

**HoleBehaviour** controls hole behaviour, managing visibility and list interaction.

**HoleViewBehaviour**: toggles visibility and handles click events, configurable via inspector-assigned events.

**ScoreBehaviour**: manages score loading and saving to score.txt. I could make this more secure or store it online but for this assignment this should suffice.

**ScoreListUIBehaviour**: displays scores in a scrollable list viewport.

**TimeBehaviour**: tracks and updates time variables for accurate display in views.

**HitBehaviour**: listens for hit events and triggers events accordingly.

**CanvasActiveBehaviour**: simplifies canvas group management, ensuring efficient handling of active/inactive states without redundant configurations in BoolMultiCompareEventBehaviour.

**VariableUIReader**: Abstract class for displaying various variables value to a TextMeshPro component. An example **FloatVariableUIBehaviour** is derrived from **VariableUIReader** and displays float values.

**ScoreListUIBehaviour**: Displays a list of the score entrys in a scroll rect. It instantiates IScoreEntry objects to display each score.

**PS. I migrated this project from plastic SCM to Git. so if branch naming seems a bit off its because of that.**
