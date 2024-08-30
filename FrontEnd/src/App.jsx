import { Routes, Route } from 'react-router-dom';
import ProfileShelf from './components/ProfileShelf';
import './App.css'
import BookDetailed from './components/BookDetailed';

export default function App() {

  return (
    <>
      <Routes>
        <Route exact path="/" element={<ProfileShelf />} />
        <Route exact path="/bookDetailed" element={<BookDetailed />}/>
      </Routes>
    </>
  )
}
