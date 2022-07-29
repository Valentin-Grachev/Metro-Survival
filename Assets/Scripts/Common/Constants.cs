

public static class Constants
{

    // Дистанция, на которой стрельба производится по позиции: позиция врага + его скорость.
    // Предполагается, что чем дальше цель - тем пуля дольше будет лететь и нужно брать с бОльшим упреждением
    public const float distancePrediction = 40f;

    // Какую часть от скорости цели берем при расчете точки приземления пули
    public const float prediction = 0.7f;

    // Направление движения по оси Х должно превысить это значение, чтобы анимация развернулась по оси Х
    public const float reversEpsilon = 0.3f;

    // Имя идентификатора всех сохранений
    public const string saveRoot = "Player";

    // Пароль шифрования
    public const string savePassword = "NuclearUnderground";

    // Случайное отклонение спавна союзников по оси Y
    public const float randomizationPosition = 1f;

    // Коэффициент индивидуализации - граница максимального отклонения при рандомизации скорости бега и атаки
    public const float individualization = 0.1f;

    // Коэффициент смещения точки приземления пули назад при выстреле баллистической пулей, чтобы накрыло как можно больше врагов
    public const float backOffset = 0.7f;


}
