import { useEffect, useState } from 'react'
import type { SyntheticEvent } from 'react'
import { getWorkItems, type WorkItem } from './api/workItemsApi'

function App() {
  const [workItems, setWorkItems] = useState<WorkItem[]>([])
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')

  useEffect(() => {
    getWorkItems().then(data => setWorkItems(data))
  }, [])

  async function handleSubmit(event: SyntheticEvent<HTMLFormElement>) {
    event.preventDefault()

    const response = await fetch('https://localhost:7055/api/workitems', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        title,
        description,
      }),
    })

    if (!response.ok) {
      throw new Error('Failed to create work item')
    }

    const createdItem: WorkItem = await response.json()

    setWorkItems(current => [...current, createdItem])
    setTitle('')
    setDescription('')
  }
  async function handleToggleComplete(item: WorkItem) {
    const response = await fetch(`https://localhost:7055/api/workitems/${item.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        title: item.title,
        description: item.description,
        isComplete: !item.isComplete,
      }),
    })

    if (!response.ok) {
      throw new Error('Failed to update work item')
    }

    setWorkItems(previousItems =>
      previousItems.map(existing =>
        existing.id === item.id ? { ...existing, isComplete: !existing.isComplete } : existing
      )
    )
  }

  return (
    <div>
      <h1>Work Tracker</h1>

      <form onSubmit={handleSubmit}>
        <div>
          <label>
            Title
            <input value={title} onChange={e => setTitle(e.target.value)} />
          </label>
        </div>

        <div>
          <label>
            Description
            <input value={description} onChange={e => setDescription(e.target.value)} />
          </label>
        </div>

        <button type="submit">Create</button>
      </form>

      <ul>
        {workItems.map(item => (
          <li key={item.id}>
            {item.title} - {item.isComplete ? 'Complete' : 'Open'}
            <button onClick={() => handleToggleComplete(item)}>Toggle</button>
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App
