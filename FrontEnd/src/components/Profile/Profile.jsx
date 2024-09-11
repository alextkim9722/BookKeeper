import { useEffect, useState, useContext } from 'react';
import axios from 'axios';

import { UserContext } from '../ProfileShelf';

import styles from './Profile.module.css'
import globalStyles from '../../Global.module.css'

export function Profile ()
{
    const userId = useContext(UserContext);

    const [editMode, setEditMode] = useState(false);
    const [userNameCharCount, setUserNameCharCount] = useState(0);
    const [username, setUserName] = useState("");
    const [user, setUser] = useState({});
    const [description, setDescription] = useState("");

    const MAX_USERNAME_CHAR_COUNT = 25;

    useEffect(() => {
        axios.get(`https://localhost:7213/api/User/GetUserById/${userId}`)
        .then(response => {
            if(response.data.success)
            {
                let body = response.data.payload;
                setUser({
                    identification: body.identification_id,
                    pKey: body.pKey,
                    username: body.username,
                    description: body.description,
                    dateJoined: body.date_joined,
                    picture: body.profile_picture,
                    booksRead: body.booksRead,
                    pagesRead: body.pagesRead
                });
            }
        })
        .catch(err => console.error(err))
    }, [username, description])

    const submitEdit = () => {
        let userModel = {
            identification_id: user.identification,
            pKey: user.pKey,
            username: username,
            description: description,
            date_joined: user.dateJoined,
            profile_picture: user.picture,
            booksRead: user.booksRead,
            pagesRead: user.pagesRead,
            books: [0,1],
            reviews: [0,1]
        };
          

        axios.put('https://localhost:7213/api/User/UpdateUser', userModel)
        .catch(err => console.error(err));

        setEditMode(false);
    }

    const changeUserName = (event) => {
        setUserName(event.target.value);
        setUserNameCharCount(username.length);
    }
    const changeDescription = (event) => {setDescription(event.target.value);}

    return(
        <div id={styles.profileRoot} className={`${globalStyles.pad}`} style={{'--padding':'30px', '--width':'30%', color:'black'}}>
            <img id={styles.profilePicture} className={`${globalStyles.round} ${globalStyles.center}`} src="/src/assets/default.png" />

            {!editMode && <h3> {user.username} </h3>}
            {editMode && (
                <>
                <div style={{width:'100%', margin:'20px auto'}} >
                    <input style={{width:'100%'}} type='text' onChange={changeUserName} />
                    <div style={{float:'right'}}>{userNameCharCount}/{MAX_USERNAME_CHAR_COUNT}</div>
                </div>
                </>
            )}

            <table>
                <tbody>
                    <tr>
                        <td> Date Joined </td>
                        <td> {user.dateJoined} </td>
                    </tr>
                    <tr>
                        <td> Books Read </td>
                        <td> {user.booksRead} Books</td>
                    </tr>
                    <tr>
                        <td> Pages Read </td>
                        <td> {user.pagesRead} Pages </td>
                    </tr>
                </tbody>
            </table>
            {!editMode && <p style={{width:'100%', height:'200px', textAlign:'justify', wordBreak:'break-word'}}>{user.description}</p>}
            {editMode && <textarea style={{width:'100%', height:'200px', margin:'10px auto'}} onChange={changeDescription} />}

            {!editMode && <div className={`${globalStyles.interactible}`} onClick={() => setEditMode(true)}>Edit Profile</div>}
            {editMode && <div className={`${globalStyles.interactible}`} onClick={() => submitEdit()}>Confirm</div>}
        </div>
    )
}