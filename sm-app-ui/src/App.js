import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.scss'
import { LeftPane, RightPane, Home } from './components'

function App() {
  return (
    <BrowserRouter>
      <div className='app'>
        <LeftPane />
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='explore' element={<div>Explore</div>} />
          <Route path='notifications' element={<div>Notifications</div>} />
          <Route path='messages' element={<div>Messages</div>} />
          <Route path='bookmarks' element={<div>Bookmarks</div>} />
          <Route path='lists' element={<div>Lists</div>} />
          <Route path='profile' element={<div>Profile</div>} />
        </Routes>
        <RightPane />
      </div>
    </BrowserRouter>
  );
}

export default App;
