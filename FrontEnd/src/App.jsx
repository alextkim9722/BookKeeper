import Bookshelf from './components/bookshelf/Bookshelf'
import Profile from './components/Profile'
import './App.css'

function App() {

  return (
    <>
      <div id="app">
        <div id="profile-column"><Profile /></div>
        <div id="shelf-column"><Bookshelf /></div>
      </div>
    </>
  )
}

export default App
