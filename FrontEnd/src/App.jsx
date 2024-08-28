import Bookshelf from './components/bookshelf/Bookshelf'
import Profile from './components/Profile'
import { BookDetailed } from './components/BookDetailed'
import { useNavigate, Routes, Route } from 'react-router-dom';
import './App.css'

export default function App() {

  return (
    <>
      <div id="app">
        <div id="profile-column"><Profile /></div>
        <div id="shelf-column"><Bookshelf /></div>

        <Routes>
        </Routes>
      </div>
    </>
  )
}
