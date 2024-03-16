<script>
	import Authorized from '../Authorized.svelte';
	import AvatarPlaceholder from '../AvatarPlaceholder.svelte';
	import LabeledInput from '../controls/LabeledInput.svelte';
	import Modal from './Modal.svelte';

	let name = 'Dmitry';
</script>

<Authorized>
	<Modal title="Профиль" buttonClasses="btn-ghost btn-circle">
		<div slot="button">
			<AvatarPlaceholder avatarClasses="rounded-full p-2"
				>{name.slice(0, 2).toUpperCase()}
			</AvatarPlaceholder>
		</div>

		<div class="flex-auto flex flex-col gap-2" slot="content">
			<h2 class="font-bold">{name}</h2>
			<form class="flex flex-col gap-3 bg-red-200 w-full">
				<div class="flex flex-row gap-3">
					<div class="flex-auto max-h-40 max-w-40 w-full">
						<AvatarPlaceholder avatarClasses="rounded-lg p-2 aspect-square">
							<span class="text-4xl">{name.slice(0, 2).toUpperCase()}</span>
						</AvatarPlaceholder>
					</div>
					<div class="grid grid-cols-5 items-center">
						<LabeledInput
							id="fullname"
							name="fullname"
							value={name}
							label="Логин:"
							type="text"
							labelClass="w-min col-span-1"
							inputClass="w-full col-span-4 bg-primary/15"
						/>
						<LabeledInput
							id="fullname"
							name="fullname"
							value={name}
							label="Имя:"
							type="text"
							labelClass="w-min col-span-1"
							inputClass="w-full col-span-4 bg-primary/15"
						/>
						<button class="btn btn-neutral btn-outline col-span-5 btn-sm" type="button">
							Сменить пароль
						</button>
					</div>
				</div>
                <button class="btn btn-primary btn-sm w-full" type="button"> Сохранить </button>
			</form>
		</div>
	</Modal>
</Authorized>
