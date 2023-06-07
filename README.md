# multiplayer-fusion

ביצענו 2 שינויים במשחק מהשיעור בסצנה rpc-5:
1. הוספנו מגן המופיע בשדה הקרב, שחקן שלוקח אותו מוגן מפגיעת לייזר.
2. הוספנו ניקוד לכל שחקן, כך שכאשר הוא פוגע בשחקן מסוים נוסף לו ניקוד.

הוספנו שינויים נוספים - עיצבנו את עולם המשחק, החלפנו שחקן והוספנו לו אנימציה, יצרנו אנימציה למגן שמסתובב סביב ציר ה-y והוספנו למגן זמן שיהיה ניתן לקחת אותו כל מס' שניות מסוים ולא פעם אחת, הוספנו מצלמה לכל שחקן, הוספנו שברגע שהבריאות של שחקן מגיעה ל-0 הוא יוצא מהמשחק ומופיעה לו סצנה של game over, השחקנים האחרים יראו אותו נעלם מהמשחק. 
קישורים לסקריפטים בהם ביצענו את השינויים : 
https://github.com/Mecurious-Games/multiplayer-fusion/blob/master/Assets/Scripts/Shield.cs
https://github.com/Mecurious-Games/multiplayer-fusion/blob/master/Assets/Scripts/HealthAndScore.cs
https://github.com/Mecurious-Games/multiplayer-fusion/blob/master/Assets/Scripts/RaycastAttack.cs
https://github.com/Mecurious-Games/multiplayer-fusion/blob/master/Assets/Scripts/PlayerSpawner.cs
https://github.com/Mecurious-Games/multiplayer-fusion/blob/master/Assets/Scripts/FirstPersonCamera.cs
