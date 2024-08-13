import React from 'react'
import { NavLink } from 'react-router-dom'
import './leftPane.scss'
import { faTwitter } from '@fortawesome/free-brands-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { bookmarksIcon, exploreIcon, homeIcon, listsIcon, messagesIcon, moreIcon, notificationsIcon, profileIcon } from './icons'


function LeftPane() {
    return (
        <>
            <div className='left-pane pl-40 sticky top-0'>
                <div className='container min-h-screen flex flex-col justify-between'>
                    <div>
                        <header className='mt-3'>
                            <FontAwesomeIcon className='text-4xl mb-3 p-2 twitter-icon ' icon={faTwitter} />
                        </header>
                        <nav>
                            <NavLink to='/' activeClassName='active'>
                                <div className='menu-item'>
                                    <span className='icon'>{homeIcon}</span> Home
                                </div>
                            </NavLink>
                            <NavLink to='/explore' activeClassName='active'>
                                <div className='menu-item'>
                                    <span className='icon'>{exploreIcon}</span> Explore
                                </div>
                            </NavLink>
                            <NavLink to='/notifications' activeClassName='active'>
                                <div className='menu-item'>
                                    <span className='icon'>{notificationsIcon}</span> Notifications
                                </div>
                            </NavLink>
                            <NavLink to='/messages' activeClassName='active'>
                                <div className='menu-item'>
                                    <span className='icon'>{messagesIcon}</span> Messages
                                </div>
                            </NavLink>
                            <NavLink to='/bookmarks' activeClassName='active'>
                                <div className='menu-item'>
                                    <span className='icon'>{bookmarksIcon}</span> Bookmarks
                                </div>
                            </NavLink>
                            <NavLink to='/lists' activeClassName='active'>
                                <div className='menu-item'>
                                    <span className='icon'>{listsIcon}</span> Lists
                                </div>
                            </NavLink>
                            <NavLink to='/profile' activeClassName='active'>
                                <div className='menu-item'>
                                    <span className='icon'>{profileIcon}</span> Profile
                                </div>
                            </NavLink>
                            <button activeClassName='active' className='more-btn'>
                                <div> {moreIcon} More </div>
                            </button>
                        </nav>

                        <button className='w-7/12 h-12 text-xl font-semibold text-white no-underline bg-sky-500 rounded-full hover:bg-sky-600'>Tweet</button>
                    </div>
                    <div>
                        <footer className='w-7/12 pl-2 pr-4'>
                            <button className='flex mb-4 py-2 gap-x-3'>
                                <div className='flex'>
                                    <img className='rounded-full w-10 h-10' alt='user' src='https://pbs.twimg.com/profile_images/1555909380117336069/MieF_3XY_bigger.jpg' />
                                </div>
                                <div className='flex flex-col text-sm'>
                                    <strong>Jane Doe</strong>
                                    <span>@janedoe</span>
                                </div>
                            </button>
                        </footer>
                    </div>
                </div>
            </div>
        </>
    )
}

export default LeftPane