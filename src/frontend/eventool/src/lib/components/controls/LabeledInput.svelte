<script lang="ts">
	let { value, ...props } = $props<{
		id: string;
		name: string;
		value: string;
		label: string | undefined;
		type: 'text' | 'password' | 'number';
		labelClass: string | '';
		inputClass: string | '';
	}>();
</script>


{#if props.label}
<label for={props.id} class={`label ${props.labelClass}`}>
    {props.label}
</label>
{/if}
<input
	id={props.id}
	name={props.name}
	type={props.type}
	bind:value={value}
	class={`input ${props.inputClass}`}
/>
