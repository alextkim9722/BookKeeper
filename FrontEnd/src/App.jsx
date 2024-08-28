import Bookshelf from './components/bookshelf/Bookshelf'
import Profile from './components/Profile'
import { useEffect, useState } from 'react'
import axios from 'axios';
import './App.css'

export default function App() {

  const [bookIds, setBookIds] = useState([]);
  const [user, setUser] = useState({});

  const getUser = async () => {
    await axios.get(`https://localhost:7213/api/User/GetUserById/${1}`)
    .then(response => {
        if(response.data.success)
        {
          let body = response.data.payload;
          setBookIds(body.books);
          setUser(
            {
              username: body.username,
              description: body.description,
              dateJoined: body.date_joined,
              picture: body.profile_picture,
              booksRead: body.booksRead,
              pagesRead: body.pagesRead
            }
          );
        }
    })
    .catch(err => console.error(err))
  }

  useEffect(() => {
    getUser();
  }, [bookIds, user])

  return (
    <>
      <div id="app">
        <div id="profile-column"><Profile user={user}/></div>
        <div id="shelf-column"><Bookshelf bookIds={bookIds}/></div>
      </div>
    </>
  )
}
