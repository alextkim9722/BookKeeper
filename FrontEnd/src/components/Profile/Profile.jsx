import styles from './Profile.module.css'
import globalStyles from '../../Global.module.css'

export function Profile (props)
{
    return(
        <div id={styles.profileRoot} className={`${globalStyles.pad}`} style={{'--padding':'30px', '--width':'30%'}}>
            <img id={styles.profilePicture} className={`${globalStyles.round} ${globalStyles.center}`} src="/src/assets/default.png" />
            <div>
                <h3>
                    {props.user.username}
                </h3>
            </div>
            <div>
                <h4>
                    {props.user.dateJoined}
                </h4>
            </div>
            <div>
                <span>Books Read</span>
                <span className={globalStyles.rightJustified}>{props.user.booksRead}</span>
            </div>
            <div>
                <span>Pages Read</span>
                <span className={globalStyles.rightJustified}>{props.user.pagesRead}</span>
            </div>
            <div>
                <p className={globalStyles.breakOverlap}>{props.user.description}</p>
            </div>
        </div>
    )
}