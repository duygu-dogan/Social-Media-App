import React from 'react'
import './rightPane.scss'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faMagnifyingGlass } from '@fortawesome/free-solid-svg-icons/faMagnifyingGlass'
import Trends from './trends/Trends'
import Recommeds from './recommends/Recommeds'

const RightPane = () => {
    return (
        <div className='right-pane pe-20'>
            <div className='sticky top-0 search-bar-bg py-3'>
                <form className='flex  mx-8 bg-gray-700 h-10 rounded-full search-bar'>
                    <div className='flex items-center'>
                        <FontAwesomeIcon className='text-white text-lg pl-2 search-icon' icon={faMagnifyingGlass} />
                    </div>

                    <input type='text' placeholder="Search" className='w-full h-full bg-gray-700 text-white pl-4 rounded-full outline-none' />
                </form>
            </div>

            <div className='trends'>
                <Trends />
            </div>

            <div className='recommends'>
                <Recommeds />
            </div>
        </div>
    )
}

export default RightPane