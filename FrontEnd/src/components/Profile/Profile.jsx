import { useState } from 'react';

import styles from './Profile.module.css'
import globalStyles from '../../Global.module.css'

export function Profile (props)
{
    const [editMode, setEditMode] = useState(false);
    const [userNameCharCount, setUserNameCharCount] = useState(0);

    const MAX_USERNAME_CHAR_COUNT = 25;

    return(
        <div id={styles.profileRoot} className={`${globalStyles.pad}`} style={{'--padding':'30px', '--width':'30%', color:'black'}}>
            <img id={styles.profilePicture} className={`${globalStyles.round} ${globalStyles.center}`} src="/src/assets/default.png" />

            {!editMode && <h3> {props.user.username} </h3>}
            {editMode && (
                <>
                <div style={{width:'100%', margin:'20px auto'}} >
                    <input style={{width:'100%'}} type='text' onChange={(event) => setUserNameCharCount(event.target.value.length)} />
                    <div style={{float:'right'}}>{userNameCharCount}/{MAX_USERNAME_CHAR_COUNT}</div>
                </div>
                </>
            )}

            <table>
                <tbody>
                    <tr>
                        <td> Date Joined </td>
                        <td> {props.user.dateJoined} </td>
                    </tr>
                    <tr>
                        <td> Books Read </td>
                        <td> {props.user.booksRead} Books</td>
                    </tr>
                    <tr>
                        <td> Pages Read </td>
                        <td> {props.user.pagesRead} Pages </td>
                    </tr>
                </tbody>
            </table>
            {!editMode && <p style={{width:'100%', height:'200px', textAlign:'justify', wordBreak:'break-word'}}>{props.user.description}</p>}
            {editMode && <textarea style={{width:'100%', height:'200px', margin:'10px auto'}}/>}

            {!editMode && <div className={`${globalStyles.interactible}`} onClick={() => setEditMode(true)}>Edit Profile</div>}
            {editMode && <div className={`${globalStyles.interactible}`} onClick={() => setEditMode(false)}>Confirm</div>}
        </div>
    )
}