using DG.Tweening;

public class NormalPanel : WindowPanel
{
    public override void ShowUI()
    {
        if (_isActive) return;
        _isActive = true;
        TitleSceneManager.Instance.canControl = false;
        SetVisible(true);
        _rectTrm.DOAnchorPos(_targetPosition, _onOffTime).SetUpdate(true);
    }

    public override void DisableUI()
    {
        if (!_isActive) return;
        
        _rectTrm.DOAnchorPos(_defaultPosition, _onOffTime).SetUpdate(true).OnComplete(() =>
        {
            SetVisible(false);
            TitleSceneManager.Instance.canControl = true;
            _isActive = false;
        });
    }
}
