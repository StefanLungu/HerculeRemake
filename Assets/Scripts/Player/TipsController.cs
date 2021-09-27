using UnityEngine;
using UnityEngine.UI;

public class TipsController : MonoBehaviour
{
    public GameObject _tipsPanel;
    public Text _tip;

    private bool moveTipShown = false;
    private bool jumpTipShown = false;
    private bool attackTipShown = false;
    private bool damageTipShown = false;
    private bool smashTipShown = false;
    private bool superPunchTipShown = false;
    private bool chestTipShown = false;
    private bool dodgeTipShown = false;
    // Start is called before the first frame update
    void Start()
    {
        _tipsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string objName = collision.gameObject.name;

        switch (objName)
        {
            case "Move":
                if (!moveTipShown)
                {
                    _tip.text = "Use 'a' and 'd' keys to move!";
                    moveTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            case "Jump":
                if (!jumpTipShown)
                {
                    _tip.text = "Use 'w' key to jump!";
                    jumpTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            case "Attack":
                if (!attackTipShown)
                {
                    _tip.text = "Use 'e' key to punch or 'spacebar' key to attack with the sword!";
                    attackTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            case "Damage":
                if (!damageTipShown)
                {
                    _tip.text = "Keep in mind that punching will deal half damage than sword!";
                    damageTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            case "Smash":
                if (!smashTipShown)
                {
                    _tip.text = "When on a bridge, press 'c' key while jumping in order to perform a smash ground!";
                    smashTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            case "SuperPunch":
                if (!superPunchTipShown)
                {
                    _tip.text = "In order to destroy obstacles, press 'q' key to perform a superpunch!";
                    superPunchTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            case "Chest":
                if (!chestTipShown)
                {
                    _tip.text = "Use 'f' key to interact with chests!";
                    chestTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            case "Dodge":
                if (!dodgeTipShown)
                {
                    _tip.text = "Use 'c' key to dodge enemy attacks!";
                    dodgeTipShown = true;
                    _tipsPanel.SetActive(true);
                }
                break;
            default:
                break;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _tipsPanel.SetActive(false);
    }
}
