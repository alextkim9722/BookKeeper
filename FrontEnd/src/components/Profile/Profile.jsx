import styles from './Profile.module.css'
import globalStyles from '../../Global.module.css'

export function Profile (props)
{
    return(
        <div id={styles.profileRoot} className={`${globalStyles.pad}`} style={{'--padding':'30px', '--width':'30%', color:'black'}}>
            <img id={styles.profilePicture} className={`${globalStyles.round} ${globalStyles.center}`} src="/src/assets/default.png" />
            <h3> {props.user.username} </h3>
            <table>
                <tr>
                    <td> Date Joined </td>
                    <td> {props.user.dateJoined} </td>
                </tr>
                <tr>
                    <td> Books Read </td>
                    <td> {props.user.booksRead} </td>
                </tr>
                <tr>
                    <td> Pages Read </td>
                    <td> {props.user.pagesRead} </td>
                </tr>
            </table>
            <div>
                <p style={{flexGrow:'1', height:'200px', textAlign:'justify', wordBreak:'break-word'}}>{props.user.description}</p>
            </div>
        </div>
    )
}