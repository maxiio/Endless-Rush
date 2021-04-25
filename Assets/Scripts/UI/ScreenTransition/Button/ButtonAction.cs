public class ButtonAction
{
    // The all actions which button's can send
    public enum Actions {
        NULL,
        PLAY,
        PAUSE,
        MENU,
        RESTART,
        CONTINUE,
        EXIT,
        GAMEOVER,
        REVIVE
    }

    public static void HandleAction(Actions action) {
        switch (action) {
            case Actions.NULL:
                throw new System.Exception("Handle the Null action");
            case Actions.RESTART:
            case Actions.PLAY:
                RestartPlay();
                UnPause();
                break;
            case Actions.CONTINUE:
                ContinuePlay();
                UnPause();
                break;
            case Actions.REVIVE:
                Revive();
                UnPause();
                break;
            case Actions.MENU:
            case Actions.GAMEOVER:
            case Actions.PAUSE:
                Pause();
                break;
            case Actions.EXIT:
                Pause();
                Exit();
                break;            
            default:
                throw new System.Exception("Handle the default action");
        }
    }

    private static void RestartPlay() {
    }

    private static void ContinuePlay() {
    }

    private static void Revive() {
    }

    private static void Pause() {
    }

    private static void UnPause() {
    }

    private static void Exit() {
    }
}
