import { useState, useEffect } from 'react';
import * as signalR from '@microsoft/signalr';
import axios from 'axios';
import TaskForm from './components/TaskForm';
import TaskTable from './components/TaskTable';

const API_URL = process.env.REACT_APP_API_URL;

export default function App() {
	const [tasks, setTasks] = useState([]);
	const [title, setTitle] = useState('');
	const [description, setDescription] = useState('');
	const [connection, setConnection] = useState(null);
	const [showModal, setShowModal] = useState(false);
	const [summary, setSummary] = useState('');
	const [loadingSummary, setLoadingSummary] = useState(false);

	useEffect(() => {
		const newConnection = new signalR.HubConnectionBuilder()
			.withUrl(`${API_URL}/taskHub`)
			.withAutomaticReconnect()
			.build();

		setConnection(newConnection);
	}, []);

	useEffect(() => {
		if (connection) {
			connection
				.start()
				.then(() => {
					console.log('Connected');

					connection.on('TaskAdded', (task) => {
						setTasks((prevTasks) => [...prevTasks, task]);
					});
				})
				.catch((error) => console.error(error));
		}

		return () => {
			if (connection) {
				connection.stop();
			}
		};
	}, [connection]);

	useEffect(() => {
		fetchTasks();
	}, []);

	const fetchTasks = async () => {
		try {
			const response = await axios.get(`${API_URL}/tasks`);
			setTasks(response.data);
		} catch (error) {
			console.error(error);
		}
	};

	const handleFormSubmit = () => {
		setTitle('');
		setDescription('');
		setShowModal(false);
	};

	const handleSummary = async () => {
		setLoadingSummary(true);
		try {
			const response = await axios.get(`${API_URL}/tasks/summary`);
			setSummary(response.data.summary);
		} catch (error) {
			console.error(error);
		} finally {
			setLoadingSummary(false);
		}
	};

	return (
		<div className="container mt-5">
			<div className="d-flex justify-content-between align-items-center mb-4">
				<h1>Tasks</h1>
				<div>
					<button
						className="btn btn-success me-2"
						onClick={handleSummary}
						disabled={loadingSummary}
					>
						{loadingSummary ? 'Loading...' : 'Summary'}
					</button>
					<button
						className="btn btn-primary"
						onClick={() => setShowModal(true)}
					>
						Add Task
					</button>
				</div>
			</div>

			{summary && (
				<div className="alert alert-info mb-4">
					<strong>Summary:</strong> {summary}
				</div>
			)}

			<TaskTable tasks={tasks} />

			{showModal && (
				<div className="modal show d-block" tabIndex="-1">
					<div className="modal-dialog">
						<div className="modal-content">
							<div className="modal-header">
								<h5 className="modal-title">Add New Task</h5>
								<button
									type="button"
									className="btn-close"
									onClick={() => setShowModal(false)}
								></button>
							</div>
							<div className="modal-body">
								<TaskForm
									title={title}
									description={description}
									onTitleChange={setTitle}
									onDescriptionChange={setDescription}
									onSubmit={handleFormSubmit}
								/>
							</div>
						</div>
					</div>
				</div>
			)}
			{showModal && <div className="modal-backdrop show"></div>}
		</div>
	);
}
